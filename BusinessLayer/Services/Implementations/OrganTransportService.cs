using BusinessLayer.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Models.ViewModels.Delivery;
using Common.Entities.OrganDelivery;
using DataLayer.Repositories.Abstractions;

namespace BusinessLayer.Services.Implementations
{
    public class OrganTransportService : IOrganTransportService
    {
        private readonly IOrganDeliveryRepository _organDeliveryRepository;
        private readonly IOrganDeliverySnapshotsRepository _deliverySnapshotsRepository;
        private readonly ITransplantOrgansService _transplantOrgansService;
        private readonly IPatientOrganRequestService _patientOrganRequestService;
        private readonly IDonorOrganRequestService _donorOrganRequestService;

        public OrganTransportService(
            IOrganDeliveryRepository organDeliveryRepository,
            IOrganDeliverySnapshotsRepository deliverySnapshotsRepository,
            ITransplantOrgansService transplantOrgansService,
            IPatientOrganRequestService patientOrganRequestService,
            IDonorOrganRequestService donorOrganRequestService)
        {
            _organDeliveryRepository = organDeliveryRepository;
            _deliverySnapshotsRepository = deliverySnapshotsRepository;
            _transplantOrgansService = transplantOrgansService;
            _patientOrganRequestService = patientOrganRequestService;
            _donorOrganRequestService = donorOrganRequestService;
        }

        public void ScheduleOrganDelivery(ScheduleDeliveryViewModel model)
        {
            var deliveryInfo = new OrganDeliveryInfo()
            {
                DonorId = model.DonorId,
                PatientId = model.PatientId,
                FromClinicId = model.FromClinicId,
                ToClinicId = model.ToClinicId,
                StartTransportTime = model.StartTransportTime
            };

            var patientOrganRequest = _patientOrganRequestService.GetById(model.PatientOrganRequestId);
            var donorRequest = patientOrganRequest.DonorOrganQuery;
            var donorOrgan = _transplantOrgansService.GetById(donorRequest.TransplantOrganId.Value);

            donorOrgan.OrganDeliveryInfo = deliveryInfo;

            //TODO: check that deliveryInfo is saved
            _transplantOrgansService.Update(donorOrgan);
        }

        public void AddOrganDeliverySnapshot(OrganStateSnapshotViewModel model)
        {
            var snapshot = new OrganDataSnapshot()
            {
                OrganDeliveryId = model.OrganDeliveryInfoId,
                Temperature = model.Temperature,
                Time = model.Time,
                Longitude = model.Longitude,
                Altitude = model.Altitude
            };

            _deliverySnapshotsRepository.Add(snapshot);
        }
    }
}
