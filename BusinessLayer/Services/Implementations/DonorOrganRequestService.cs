using BusinessLayer.Models.Enums;
using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using DataLayer.Entities;
using DataLayer.Entities.Identity;
using DataLayer.Entities.Organ;
using DataLayer.Entities.OrganQueries;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BusinessLayer.Services.Implementations
{
    public class DonorOrganRequestService : IDonorOrganRequestService
    {
        private readonly IDonorOrganRequestRepository _donorOrganRequestRepository;
        private readonly IMedicalExamsRepository _medicalExamsRepository;
        private readonly IOrganInfoService _organInfoService;
        private readonly IUsersService _usersService;
        private readonly IClinicsService _clinicsService;
        private readonly ITransplantOrgansService _transplantOrgansService;

        public DonorOrganRequestService(
            IOrganInfoService organInfoService,
            IUsersService usersService,
            IClinicsService clinicsService,
            ITransplantOrgansService transplantOrgansService,
            IDonorOrganRequestRepository donorOrganRequestRepository,
            IMedicalExamsRepository medicalExamsRepository)
        {
            _donorOrganRequestRepository = donorOrganRequestRepository;
            _medicalExamsRepository = medicalExamsRepository;
            _organInfoService = organInfoService;
            _usersService = usersService;
            _clinicsService = clinicsService;
            _transplantOrgansService = transplantOrgansService;
        }

        public DonorOrganQuery GetById(int id)
        {
            return _donorOrganRequestRepository.GetById(id);
        }

        public void RegisterDonorOrganRequest(DonorOrganRequestViewModel request)
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

            RegisterDonorOrganRequestInner(request);
        }

        private void RegisterDonorOrganRequestInner(DonorOrganRequestViewModel request)
        {
            //TODO: use transaction here

            //TODO: use mapping
            var userInfo = new UserInfo()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                City = request.City,
                Country = request.Country,
                ZipCode = request.ZipCode,
                PhoneNumber = request.PhoneNumber
            };

            User user;
            try
            {
                user = _usersService.RegisterUserAsDonor(userInfo);
            }
            catch (Exception ex)
            {
                //TODO: Log
                Debug.WriteLine(ex.Message);
                throw;
            }

            var donorOrganRequest = new DonorOrganQuery
            {
                DonorInfoId = user.UserInfo.UserInfoId,
                OrganInfoId = request.OrganInfoId,
                Message = request.Message,
                Status = (int)DonorRequestStatuses.PendingMedicalExamination
            };

            _donorOrganRequestRepository.Save(donorOrganRequest);
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
            var donorOrganRequest = _donorOrganRequestRepository.GetById(model.DonorOrganQueryId);
            if (donorOrganRequest == null)
            {
                //TODO: handle
                return;
            }

            var medicalExamEntity = new DonorMedicalExam()
            {
                ClinicId = model.ClinicId,
                DonorOrganQueryId = model.DonorOrganQueryId,
                ScheduledAt = model.ScheduledDateTime,
                Status = (int)MedicalExamStatuses.Scheduled
            };

            donorOrganRequest.Status = (int)DonorRequestStatuses.ScheduledMedicalExamination;

            //TODO: better use transaction
            _medicalExamsRepository.Add(medicalExamEntity);
            _donorOrganRequestRepository.Update(donorOrganRequest);
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
            var donorOrganQuery = _donorOrganRequestRepository.GetById(model.DonorOrganQueryId);
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
                    Status = (int)TransplantOrganStatuses.ScheduledRetrieving,
                    Created = DateTime.UtcNow,
                    CreatedBy = "Default"
                };
            }
            
            donorOrganQuery.Status = (int)(model.MedicalExamStatus == MedicalExamStatuses.Pass
                ? DonorRequestStatuses.NeedToScheduleTimeForOrganRetrieving
                : DonorRequestStatuses.FailedMedicalExamination);

            var exam = donorOrganQuery.DonorMedicalExams.LastOrDefault();
            exam.Results = model.MedicalExamResults;
            exam.Status = (int)model.MedicalExamStatus;

            //TODO: use transaction here
            _medicalExamsRepository.Update(exam);
            _donorOrganRequestRepository.Update(donorOrganQuery);
        }

        public void ChangeStatusTo(int donorRequestId, DonorRequestStatuses status)
        {
            DonorOrganQuery request = _donorOrganRequestRepository.GetById(donorRequestId);
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            request.Status = (int)status;

            _donorOrganRequestRepository.Update(request);
        }
    }
}
