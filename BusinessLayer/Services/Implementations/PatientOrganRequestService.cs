﻿using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Utils;
using DataLayer.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using Common.Entities;
using Common.Entities.Identity;
using Common.Entities.OrganQueries;
using Common.Enums;

namespace BusinessLayer.Services.Implementations
{
    public class PatientOrganRequestService : IPatientOrganRequestService
    {
        private const string UserCreationFailedErrorMessage = "Error while creating a user";

        private readonly IDonorOrganRequestService _donorOrganRequestService;
        private readonly IPatientOrganQueriesRepository _patientOrganQueriesRepository;
        private readonly IOrganInfoService _organInfoService;
        private readonly IUserInfoService _userInfoService;
        private readonly UserManager<AppUser> _userManager;

        public PatientOrganRequestService(
            IPatientOrganQueriesRepository patientOrganQueriesRepository,
            IOrganInfoService organInfoService,
            IUserInfoService userInfoService,
            IDonorOrganRequestService donorOrganRequestService,
            UserManager<AppUser> userManager)
        {
            _userInfoService = userInfoService;
            _patientOrganQueriesRepository = patientOrganQueriesRepository;
            _organInfoService = organInfoService;
            _donorOrganRequestService = donorOrganRequestService;
            _userManager = userManager;
        }

        public PatientOrganQuery GetById(int patientOrganRequestId)
        {
            return _patientOrganQueriesRepository.GetById(patientOrganRequestId);
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
            if (user == null)
            {
                user = CreatePatient(model);
            }

            var patientInfo = _userInfoService.GetUserInfoByUserId(user.Id);

            var patientOrganQuery = new PatientOrganQuery()
            {
                OrganInfoId = model.OrganInfoId,
                PatientInfoId = patientInfo.UserInfoId,
                Priority = model.QueryPriority,
                Message = model.AdditionalInfo,
                Status = PatientRequestStatuses.AwaitingForDonor
            };

            _patientOrganQueriesRepository.Add(patientOrganQuery);

            //TODO: send email to patient email with credentials
            //TODO: send email to clinic that query has been added
        }

        private AppUser CreatePatient(PatientOrganRequestViewModel model)
        {
            var patientInfo = new UserInfo()
            {
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Country = model.Country,
                City = model.City,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                ZipCode = model.ZipCode
            };
            
            AppUser user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email,
                Created = DateTime.UtcNow,
                CreatedBy = "CurrentUser",
                EmailConfirmed = true,
                PhoneNumber = model.PhoneNumber,
                PhoneNumberConfirmed = true,
                UserInfo = patientInfo
            };

            var password = PasswordHasher.GeneratePassword();
            var result = _userManager.CreateAsync(user, password).Result;

            if (result.Succeeded)
            {
                result = _userManager.AddToRoleAsync(user, RolesConstants.Patient).Result;
            }
            else
            {
                throw new InvalidOperationException(UserCreationFailedErrorMessage);
            }

            if (!result.Succeeded)
            {
                _userManager.DeleteAsync(user).Wait();
                throw new InvalidOperationException(UserCreationFailedErrorMessage);
            }

            return user;
        }

        public void ChangePatientOrganQueryStatus(int patientOrganQueryId, PatientRequestStatuses status)
        {
            PatientOrganQuery patientOrganQuery = _patientOrganQueriesRepository.GetById(patientOrganQueryId);
            if (patientOrganQuery == null)
            {
                throw new ArgumentException(nameof(patientOrganQueryId));
            }

            if (!Enum.IsDefined(typeof(PatientQueryPriority), patientOrganQuery.Priority))
            {
                patientOrganQuery.Priority = PatientQueryPriority.Normal;
            }

            patientOrganQuery.Status = PatientRequestStatuses.AwaitingForDonor;

            _patientOrganQueriesRepository.Update(patientOrganQuery);

            //TODO: send email to clinic that query status has been changed
        }

        public void AssignToDonorOrganQuery(int patientOrganQueryId, int donorOrganQueryId)
        {
            PatientOrganQuery patientOrganQuery = _patientOrganQueriesRepository.GetById(patientOrganQueryId);
            if (patientOrganQuery == null)
            {
                throw new ArgumentException(nameof(patientOrganQueryId));
            }

            //TODO: get PURE entity
            DonorOrganQuery donorOrganQuery = _donorOrganRequestService.GetById(donorOrganQueryId);
            if (donorOrganQuery == null)
            {
                throw new ArgumentException(nameof(donorOrganQueryId));
            }

            patientOrganQuery.DonorOrganQuery = donorOrganQuery;
            patientOrganQuery.Status = PatientRequestStatuses.AwaitingForTransplanting;

            //TODO: check if donorOrganQuery saved 
            _patientOrganQueriesRepository.Update(patientOrganQuery);

            //TODO: send email to clinic/patient/donor that query status has been changed
        }
    }
}
