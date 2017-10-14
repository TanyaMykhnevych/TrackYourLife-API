using BusinessLayer.Models.Enums;
using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using DataLayer.Entities;
using DataLayer.Entities.Organ;
using DataLayer.Entities.OrganQueries;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services.Implementations
{
    public class PatientOrganRequestService : IPatientOrganRequestService
    {
        private readonly IDonorOrganRequestService _donorOrganRequestService;
        private readonly IPatientOrganQueriesRepository _patientOrganQueriesRepository;
        private readonly IOrganInfoService _organInfoService;
        private readonly IUsersService _usersService;

        public PatientOrganRequestService(
            IPatientOrganQueriesRepository patientOrganQueriesRepository,
            IOrganInfoService organInfoService,
            IDonorOrganRequestService donorOrganRequestService,
            IUsersService usersService)
        {
            _patientOrganQueriesRepository = patientOrganQueriesRepository;
            _organInfoService = organInfoService;
            _donorOrganRequestService = donorOrganRequestService;
            _usersService = usersService;
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

            var donorInfo = new UserInfo()
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
            //TODO: use transaction
            var user = _usersService.RegisterUserAsPatient(donorInfo);

            var patientOrganQuery = new PatientOrganQuery()
            {
                OrganInfoId = model.OrganInfoId,
                PatientInfoId = user.UserInfo.UserInfoId,
                Priority = (int)model.QueryPriority,
                Message = model.AdditionalInfo,
                Status = (int)PatientQueryStatuses.AwaitingForDonor
            };

            _patientOrganQueriesRepository.Save(patientOrganQuery);

            //TODO: send email to clinic that query has been added
        }

        public void ChangePatientOrganQueryStatus(int patientOrganQueryId, PatientQueryStatuses status)
        {
            PatientOrganQuery patientOrganQuery = _patientOrganQueriesRepository.GetById(patientOrganQueryId);
            if (patientOrganQuery == null)
            {
                throw new ArgumentException(nameof(patientOrganQueryId));
            }

            if (!Enum.IsDefined(typeof(PatientQueryPriority), patientOrganQuery.Priority))
            {
                patientOrganQuery.Priority = (int)PatientQueryPriority.Normal;
            }

            patientOrganQuery.Status = (int)PatientQueryStatuses.AwaitingForDonor;

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
            patientOrganQuery.Status = (int)PatientQueryStatuses.AwaitingForTransplanting;

            //TODO: check if donorOrganQuery saved 
            _patientOrganQueriesRepository.Update(patientOrganQuery);

            //TODO: send email to clinic/patient/donor that query status has been changed
        }
    }
}
