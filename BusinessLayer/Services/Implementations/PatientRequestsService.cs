using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using DataLayer.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using Common.Entities;
using Common.Entities.Identity;
using Common.Entities.OrganRequests;
using Common.Enums;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Common.Models;

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

        public IList<PatientRequest> GetPatientRequests()
        {
            return _patientRequestsRepository.GetAll(
           include: x => x.Include(p => p.RequestsRelation)
               .Include(p => p.OrganInfo)
               .Include(p => p.PatientInfo)
               .Include(p => p.RequestsRelation))
               .OrderBy(p => p.OrganInfo.Name)
                    .ThenByDescending(p => p.Priority)
                    .ThenBy(p => p.Created)
                    .ToList();
        }

        public IList<PatientRequest> GetPatientRequestsByUsername(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            return _patientRequestsRepository.GetAll(
                predicate: dr => dr.PatientInfo.AppUserId == user.Id,
                include: x => x.Include(p => p.RequestsRelation)
                    .Include(p => p.OrganInfo)
                    .Include(p => p.PatientInfo))
                    .OrderBy(p => p.OrganInfo.Name)
                    .ThenByDescending(p => p.Priority)
                    .ThenBy(p => p.Created)
                    .ToList();
        }


        public IList<PatientRequest> GetReadyToTransportPatientRequests()
        {
            return _patientRequestsRepository.GetAll(
                predicate: dr =>
                    dr.Status == PatientRequestStatuses.AwaitingForTransplanting
                    && dr.RequestsRelation != null
                    && dr.RequestsRelation.DonorRequest != null
                    && dr.RequestsRelation.DonorRequest.TransplantOrgan != null,
                include: x => x.Include(p => p.RequestsRelation)
                    .Include(p => p.OrganInfo)
                    .Include(p => p.PatientInfo))
                    .OrderBy(p => p.OrganInfo.Name)
                    .ThenByDescending(p => p.Priority)
                    .ThenBy(p => p.Created)
                    .ToList();
        }

        public PatientRequest GetById(int patientOrganRequestId)
        {
            return _patientRequestsRepository.GetById(patientOrganRequestId);
        }

        public PatientRequest GetDetailedById(int id)
        {
            return _patientRequestsRepository.GetDetailedById(id);
        }

        public bool HasPatientRequest(string id, int patientRequestId)
        {
            var userInfo = _userInfoService.GetUserInfoByUserId(id);
            if (userInfo == null) return false;

            return _patientRequestsRepository.Any(dr =>
                dr.PatientInfoId == userInfo.UserInfoId && dr.Id == patientRequestId);
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

        public void UpdatePatientRequestWithPatient(EditPatientRequestModel model)
        {
            var patRequest = _patientRequestsRepository.GetById(model.PatientRequestId);
            if (patRequest == null)
            {
                return;
            }

            patRequest.Message = model.Message;
            _patientRequestsRepository.Update(patRequest);

            if (patRequest.PatientInfoId.HasValue)
            {
                var patient = _userInfoService.GetUserInfoById(patRequest.PatientInfoId.Value);

                patient.AddressLine1 = model.PatientAddressLine1;
                patient.PhoneNumber = model.PatientPhoneNumber;

                _userInfoService.Update(patient);
            }
        }
    }
}
