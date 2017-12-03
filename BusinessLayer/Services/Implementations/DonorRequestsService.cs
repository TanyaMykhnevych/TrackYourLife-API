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
        private readonly IMedicalExamsService _medicalExamsService;
        private readonly IOrganInfoService _organInfoService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserInfoService _userInfoService;
        private readonly IPatientRequestsService _patientRequestsService;

        public DonorRequestsService(
            IOrganInfoService organInfoService,
            IUserInfoService userInfoService,
            IDonorRequestsRepository donorRequestsRepository,
            UserManager<AppUser> userManager,
            IMedicalExamsService medicalExamsService,
            IPatientRequestsService patientRequestsService)
        {
            _donorRequestsRepository = donorRequestsRepository;
            _medicalExamsService = medicalExamsService;
            _organInfoService = organInfoService;
            _userManager = userManager;
            _userInfoService = userInfoService;
            _patientRequestsService = patientRequestsService;
        }

        public IList<DonorRequest> GetDonorRequests()
        {
            return _donorRequestsRepository.GetAll(
                include: x => x.Include(dr => dr.DonorMedicalExams)
                    .Include(dr => dr.OrganInfo)
                    .Include(dr => dr.DonorInfo)
                    .Include(dr => dr.RequestsRelation)
                    .Include(dr => dr.TransplantOrgan))
                    .OrderBy(dr => dr.TransplantOrgan?.OrganInfo?.Name)
                    .ToList();
        }

        public IList<DonorRequest> GetDonorRequestsByUsername(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            return _donorRequestsRepository.GetAll(
                predicate: dr => dr.DonorInfo.AppUserId == user.Id,
                include: x => x.Include(dr => dr.DonorMedicalExams)
                    .Include(dr => dr.OrganInfo)
                    .Include(dr => dr.DonorInfo)
                    .Include(dr => dr.RequestsRelation)
                    .Include(dr => dr.TransplantOrgan))
                    .OrderBy(dr => dr.TransplantOrgan?.OrganInfo?.Name)
                    .ToList();
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

            _medicalExamsService.AddMedicalExam(medicalExamEntity);
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
            var donorRequest = _donorRequestsRepository.GetById(model.DonorRequestId);
            if (donorRequest == null)
            {
                //TODO: handle
                return;
            }

            donorRequest.Status = model.MedicalExamStatus == MedicalExamStatuses.Pass
                ? DonorRequestStatuses.AwaitingForPatientRequest
                : DonorRequestStatuses.FailedMedicalExamination;


            var exam = _medicalExamsService.GetLastMedicalExamByDonorRequestId(donorRequest.Id);

            exam.Results = model.MedicalExamResults;
            exam.Status = model.MedicalExamStatus;

            if (model.MedicalExamStatus == MedicalExamStatuses.Pass && !donorRequest.TransplantOrganId.HasValue)
            {
                donorRequest.TransplantOrgan = new TransplantOrgan()
                {
                    UserInfoId = donorRequest.DonorInfoId,
                    OrganInfoId = donorRequest.OrganInfoId,
                    ClinicId = exam.ClinicId,
                    Status = TransplantOrganStatuses.AwaitingSchedulingRetrieving,
                    Created = DateTime.UtcNow,
                    CreatedBy = CurrentUserHolder.GetCurrentUserName()
                };
            }

            _medicalExamsService.UpdateMedicalExam(exam);
            _donorRequestsRepository.Update(donorRequest);
        }

        public void FinishDonorRequestSuccessfully(int donorRequestId)
        {
            var donorRequest = _donorRequestsRepository.GetDetailedById(donorRequestId);
            if (donorRequest?.RequestsRelation == null)
            {
                return;
            }

            var requestsRelation = donorRequest.RequestsRelation;

            ChangeStatusTo(donorRequestId, DonorRequestStatuses.FinishedSuccessfully);
            _patientRequestsService.ChangePatientRequestStatus(requestsRelation.PatientRequestId, PatientRequestStatuses.FinishedSuccessfully);
        }

        public void FinishDonorRequestFailed(int donorRequestId)
        {
            var donorRequest = _donorRequestsRepository.GetDetailedById(donorRequestId);
            if (donorRequest?.RequestsRelation == null)
            {
                return;
            }

            var requestsRelation = donorRequest.RequestsRelation;

            ChangeStatusTo(donorRequestId, DonorRequestStatuses.FinishedFailed);
            _patientRequestsService.ChangePatientRequestStatus(requestsRelation.PatientRequestId, PatientRequestStatuses.FinishedFailed);
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