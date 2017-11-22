﻿using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using DataLayer.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using Common.Entities;
using Common.Entities.Identity;
using Common.Entities.OrganRequests;
using Common.Enums;

namespace BusinessLayer.Services.Implementations
{
    public class PatientRequestsService : IPatientRequestsService
    {
        private readonly IDonorRequestsRepository _donorRequestsRepository;
        private readonly IPatientRequestsRepository _patientRequestsRepository;
        private readonly IRequestsRelationsRepository _requestsRelationsRepository;
        private readonly IOrganInfoService _organInfoService;
        private readonly IUserInfoService _userInfoService;
        private readonly UserManager<AppUser> _userManager;

        public PatientRequestsService(
            IPatientRequestsRepository patientRequestsRepository,
            IDonorRequestsRepository donorRequestsRepository,
            IRequestsRelationsRepository requestsRelationsRepository,
            IOrganInfoService organInfoService,
            IUserInfoService userInfoService,
            UserManager<AppUser> userManager)
        {
            _patientRequestsRepository = patientRequestsRepository;
            _donorRequestsRepository = donorRequestsRepository;
            _requestsRelationsRepository = requestsRelationsRepository;
            _organInfoService = organInfoService;
            _userInfoService = userInfoService;
            _userManager = userManager;
        }

        public PatientRequest GetById(int patientOrganRequestId)
        {
            return _patientRequestsRepository.GetById(patientOrganRequestId);
        }

        public void AddPatientRequestToQueue(PatientOrganRequestViewModel model)
        {
            var isOrganInfoExist = _organInfoService.IfOrganInfoExists(model.OrganInfoId);
            if (!isOrganInfoExist)
                throw new ArgumentException(nameof(model.OrganInfoId));

            if (!Enum.IsDefined(typeof(PatientRequestPriority), model.QueryPriority))
                model.QueryPriority = PatientRequestPriority.Normal;

            var user = _userManager.FindByEmailAsync(model.Email).Result;
            var patientUserInfo = user == null 
                ? _userInfoService.RegisterPatient(model) 
                : _userInfoService.GetUserInfoByUserId(user.Id);

            var patientRequest = new PatientRequest()
            {
                OrganInfoId = model.OrganInfoId,
                PatientInfoId = patientUserInfo.UserInfoId,
                Priority = model.QueryPriority,
                Message = model.AdditionalInfo,
                Status = PatientRequestStatuses.AwaitingForDonor
            };

            _patientRequestsRepository.Add(patientRequest);

            //TODO: send email to patient email with credentials
            //TODO: send email to clinic that query has been added
        }

        public void ChangePatientRequestStatus(int patientRequestId, PatientRequestStatuses status)
        {
            var patientOrganQuery = _patientRequestsRepository.GetById(patientRequestId);
            if (patientOrganQuery == null)
            {
                throw new ArgumentException(nameof(patientRequestId));
            }

            if (!Enum.IsDefined(typeof(PatientRequestPriority), patientOrganQuery.Priority))
                patientOrganQuery.Priority = PatientRequestPriority.Normal;

            patientOrganQuery.Status = status;

            _patientRequestsRepository.Update(patientOrganQuery);

            //TODO: send email to clinic that query status has been changed
        }
    }
}
