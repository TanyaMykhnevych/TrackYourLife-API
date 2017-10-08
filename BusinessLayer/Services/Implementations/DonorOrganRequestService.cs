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
using System.Text;

namespace BusinessLayer.Services.Implementations
{
    public class DonorOrganRequestService : IDonorOrganRequestService
    {
        private readonly IDonorOrganRequestRepository _donorOrganRequestRepository;
        private readonly IOrganInfoService _organInfoService;
        private readonly IUsersService _usersService;
        private readonly IClinicsService _clinicsService;
        private readonly ITransplantOrgansService _transplantOrgansService;

        public DonorOrganRequestService(
            IOrganInfoService organInfoService,
            IUsersService usersService,
            IClinicsService clinicsService,
            ITransplantOrgansService transplantOrgansService,
            IDonorOrganRequestRepository donorOrganRequestRepository)
        {
            _donorOrganRequestRepository = donorOrganRequestRepository;
            _organInfoService = organInfoService;
            _usersService = usersService;
            _clinicsService = clinicsService;
            _transplantOrgansService = transplantOrgansService;
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
                throw;
            }

            var donorOrganRequest = new DonorOrganQuery
            {
                DonorInfoId = user.UserInfo.UserInfoId,
                OrganInfoId = request.OrganInfoId,
                Message = request.Message,
                Status = (int)DonorRequestStatuses.PendingMedicalExamination,
                Created = DateTime.UtcNow,
                //TODO: determine current user
                CreatedBy = "Default"
            };

            _donorOrganRequestRepository.Save(donorOrganRequest);
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

        public void ChangeStatusAndFillTransplantOrgan(int donorRequestId, TransplantOrgan transplantOrgan)
        {
            DonorOrganQuery request = _donorOrganRequestRepository.GetById(donorRequestId);
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            //TODO: choose appropriate clinic
            Clinic clinic = _clinicsService.GetFirst();

            transplantOrgan.ClinicId = clinic.Id;
            transplantOrgan.OrganInfoId = request.OrganInfoId;
            transplantOrgan.UserInfoId = request.DonorInfoId;

            //TODO: Use transaction to save transplantOrgan && update request
            _transplantOrgansService.Save(transplantOrgan);

            request.Status = (int) DonorRequestStatuses.NeedToScheduleTimeForTransplanting;

            _donorOrganRequestRepository.Update(request);
        }
    }
}
