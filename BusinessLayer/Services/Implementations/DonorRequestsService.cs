using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using DataLayer.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BusinessLayer.Models.ViewModels.Donor;
using Common.Entities;
using Common.Entities.Identity;
using Common.Entities.Organ;
using Common.Entities.OrganRequests;
using Common.Enums;
using Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations
{
    public class DonorRequestsService : IDonorRequestsService
    {
        private readonly IDonorRequestsRepository _donorRequestsRepository;
        private readonly IMedicalExamsRepository _medicalExamsRepository;
        private readonly IOrganInfoService _organInfoService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserInfoService _userInfoService;

        public DonorRequestsService(
            IOrganInfoService organInfoService,
            IUserInfoService userInfoService,
            IDonorRequestsRepository donorRequestsRepository,
            UserManager<AppUser> userManager,
            IMedicalExamsRepository medicalExamsRepository)
        {
            _donorRequestsRepository = donorRequestsRepository;
            _medicalExamsRepository = medicalExamsRepository;
            _organInfoService = organInfoService;
            _userManager = userManager;
            _userInfoService = userInfoService;
        }

        public IList<DonorRequest> GetDonorRequests()
        {
            return _donorRequestsRepository.GetAll(
                include: x => x.Include(dr => dr.DonorMedicalExams)
                .Include(dr => dr.PatientRequest)
                .Include(dr => dr.OrganInfo)
                .Include(dr => dr.TransplantOrgan));
        }

        public IList<DonorRequest> GetDonorRequestsByUsername(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            return _donorRequestsRepository.GetAll(
                predicate: dr => dr.DonorInfo.AppUserId == user.Id,
                include: x => x.Include(dr => dr.DonorMedicalExams)
                    .Include(dr => dr.PatientRequest)
                    .Include(dr => dr.OrganInfo)
                    .Include(dr => dr.TransplantOrgan));
        }

        public DonorRequest GetById(int id)
        {
            return _donorRequestsRepository.GetById(id);
        }

        public DonorRequest GetDetailedById(int id)
        {
            return _donorRequestsRepository.GetDetailedById(id);
        }

        public bool HasDonorRequest(string id, int donorRequestId)
        {
            var userInfo = _userInfoService.GetUserInfoByUserId(id);
            if (userInfo == null) return false;

            return _donorRequestsRepository.Any(dr =>
                dr.DonorInfoId == userInfo.UserInfoId && dr.Id == donorRequestId);
        }

        public void RegisterDonorOrganRequest(DonorRequestViewModel request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (!_organInfoService.IfOrganInfoExists(request.OrganInfoId))
            {
                throw new ArgumentException("Organ Info does not exist.");
            }

            //TODO: validate contacts

            var user = _userManager.FindByEmailAsync(request.Email).Result;
            UserInfo donorUserInfo = user == null 
                ? _userInfoService.RegisterDonor(request)
                : _userInfoService.GetUserInfoByUserId(user.Id);

            RegisterDonorOrganRequestInner(request, donorUserInfo);
        }

        private void RegisterDonorOrganRequestInner(DonorRequestViewModel request, UserInfo donorUserInfo)
        {
            var donorOrganRequest = new DonorRequest
            {
                DonorInfoId = donorUserInfo.UserInfoId,
                OrganInfoId = request.OrganInfoId,
                Message = request.Message,
                Status = DonorRequestStatuses.PendingMedicalExamination
            };

            _donorRequestsRepository.Add(donorOrganRequest);
        }

        public void ScheduleMedicalExam(ScheduleMedicalExamViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            ScheduleMedicalExamInner(model);
        }

        private void ScheduleMedicalExamInner(ScheduleMedicalExamViewModel model)
        {
            var donorOrganRequest = _donorRequestsRepository.GetById(model.DonorRequestId);
            if (donorOrganRequest == null)
            {
                //TODO: handle
                return;
            }

            var medicalExamEntity = new DonorMedicalExam()
            {
                ClinicId = model.ClinicId,
                DonorRequestId = model.DonorRequestId,
                ScheduledAt = model.ScheduledDateTime,
                Status = MedicalExamStatuses.Scheduled
            };

            donorOrganRequest.Status = DonorRequestStatuses.ScheduledMedicalExamination;

            //TODO: better use transaction
            _medicalExamsRepository.Add(medicalExamEntity);
            _donorRequestsRepository.Update(donorOrganRequest);
        }

        /// <summary>
        /// Creates TransplantOrgan entity (status ScheduledTransplanting...);
        /// Updates DonorMedicalExam entity;
        /// </summary>
        public void UpdateMedicalExamResults(MedicalExamResultViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            UpdateMedicalExamResultsInner(model);
        }

        private void UpdateMedicalExamResultsInner(MedicalExamResultViewModel model)
        {
            var donorOrganQuery = _donorRequestsRepository.GetById(model.DonorOrganQueryId);
            if (donorOrganQuery == null)
            {
                //TODO: handle
                return;
            }

            if (model.MedicalExamStatus == MedicalExamStatuses.Pass)
            {
                //TODO: check saving TransplantOrgan entity
                donorOrganQuery.TransplantOrgan = new TransplantOrgan()
                {
                    UserInfoId = donorOrganQuery.DonorInfoId,
                    OrganInfoId = donorOrganQuery.OrganInfoId,
                    Status = TransplantOrganStatuses.ScheduledRetrieving,
                    Created = DateTime.UtcNow,
                    CreatedBy = CurrentUserHolder.GetCurrentUserName()
                };
            }

            donorOrganQuery.Status = model.MedicalExamStatus == MedicalExamStatuses.Pass
                ? DonorRequestStatuses.AwaitingForPatientRequest
                : DonorRequestStatuses.FailedMedicalExamination;

            var exam = donorOrganQuery.DonorMedicalExams.LastOrDefault();
            exam.Results = model.MedicalExamResults;
            exam.Status = model.MedicalExamStatus;

            //TODO: use transaction here
            _medicalExamsRepository.Update(exam);
            _donorRequestsRepository.Update(donorOrganQuery);
        }

        public void ChangeStatusTo(int donorRequestId, DonorRequestStatuses status)
        {
            DonorRequest request = _donorRequestsRepository.GetById(donorRequestId);
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            request.Status = status;

            _donorRequestsRepository.Update(request);
        }
    }
}