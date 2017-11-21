using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using DataLayer.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using Common.Entities;
using Common.Entities.Identity;
using Common.Entities.OrganQueries;
using Common.Enums;

namespace BusinessLayer.Services.Implementations
{
    public class PatientRequestsService : IPatientRequestsService
    {
        private readonly IDonorRequestsService _donorRequestsService;
        private readonly IPatientRequestsRepository _patientRequestsRepository;
        private readonly IOrganInfoService _organInfoService;
        private readonly IUserInfoService _userInfoService;
        private readonly UserManager<AppUser> _userManager;

        public PatientRequestsService(
            IPatientRequestsRepository patientRequestsRepository,
            IOrganInfoService organInfoService,
            IUserInfoService userInfoService,
            IDonorRequestsService donorRequestsService,
            UserManager<AppUser> userManager)
        {
            _userInfoService = userInfoService;
            _patientRequestsRepository = patientRequestsRepository;
            _organInfoService = organInfoService;
            _donorRequestsService = donorRequestsService;
            _userManager = userManager;
        }

        public PatientRequest GetById(int patientOrganRequestId)
        {
            return _patientRequestsRepository.GetById(patientOrganRequestId);
        }

        public void AddPatientOrganQueryToQueue(PatientOrganRequestViewModel model)
        {

            bool isOrganInfoExist = _organInfoService.IfOrganInfoExists(model.OrganInfoId);
            if (!isOrganInfoExist)
            {
                throw new ArgumentException(nameof(model.OrganInfoId));
            }

            if (!Enum.IsDefined(typeof(PatientQueryPriority), model.QueryPriority))
            {
                model.QueryPriority = PatientQueryPriority.Normal;
            }
            
            AppUser user = _userManager.FindByEmailAsync(model.Email).Result;
            UserInfo patientUserInfo = user == null 
                ? _userInfoService.RegisterPatient(model) 
                : _userInfoService.GetUserInfoByUserId(user.Id);

            var patientOrganQuery = new PatientRequest()
            {
                OrganInfoId = model.OrganInfoId,
                PatientInfoId = patientUserInfo.UserInfoId,
                Priority = model.QueryPriority,
                Message = model.AdditionalInfo,
                Status = PatientRequestStatuses.AwaitingForDonor
            };

            _patientRequestsRepository.Add(patientOrganQuery);

            //TODO: send email to patient email with credentials
            //TODO: send email to clinic that query has been added
        }

        public void ChangePatientOrganQueryStatus(int patientOrganQueryId, PatientRequestStatuses status)
        {
            PatientRequest patientOrganQuery = _patientRequestsRepository.GetById(patientOrganQueryId);
            if (patientOrganQuery == null)
            {
                throw new ArgumentException(nameof(patientOrganQueryId));
            }

            if (!Enum.IsDefined(typeof(PatientQueryPriority), patientOrganQuery.Priority))
            {
                patientOrganQuery.Priority = PatientQueryPriority.Normal;
            }

            patientOrganQuery.Status = PatientRequestStatuses.AwaitingForDonor;

            _patientRequestsRepository.Update(patientOrganQuery);

            //TODO: send email to clinic that query status has been changed
        }

        public void AssignToDonorOrganQuery(int patientOrganQueryId, int donorOrganQueryId)
        {
            PatientRequest patientOrganQuery = _patientRequestsRepository.GetById(patientOrganQueryId);
            if (patientOrganQuery == null)
            {
                throw new ArgumentException(nameof(patientOrganQueryId));
            }

            //TODO: get PURE entity
            DonorRequest donorOrganQuery = _donorRequestsService.GetById(donorOrganQueryId);
            if (donorOrganQuery == null)
            {
                throw new ArgumentException(nameof(donorOrganQueryId));
            }

            patientOrganQuery.DonorRequest = donorOrganQuery;
            patientOrganQuery.Status = PatientRequestStatuses.AwaitingForTransplanting;

            //TODO: check if donorOrganQuery saved 
            _patientRequestsRepository.Update(patientOrganQuery);

            //TODO: send email to clinic/patient/donor that query status has been changed
        }
    }
}
