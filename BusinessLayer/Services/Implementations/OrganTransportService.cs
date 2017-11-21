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
        private readonly IOrganDeliverySnapshotsRepository _deliverySnapshotsRepository;
        private readonly ITransplantOrgansService _transplantOrgansService;
        private readonly IPatientRequestsService _patientRequestsService;

        public OrganTransportService(
            IOrganDeliverySnapshotsRepository deliverySnapshotsRepository,
            ITransplantOrgansService transplantOrgansService,
            IPatientRequestsService patientRequestsService)
        {
            _deliverySnapshotsRepository = deliverySnapshotsRepository;
            _transplantOrgansService = transplantOrgansService;
            _patientRequestsService = patientRequestsService;
        }

        public void ScheduleOrganDelivery(ScheduleDeliveryViewModel model)
        {
            var deliveryInfo = new OrganDeliveryInfo
            {
                DonorId = model.DonorId,
                PatientId = model.PatientId,
                FromClinicId = model.FromClinicId,
                ToClinicId = model.ToClinicId,
                StartTransportTime = model.StartTransportTime
            };

            var patientOrganRequest = _patientRequestsService.GetById(model.PatientOrganRequestId);
            var donorRequest = patientOrganRequest.DonorRequest;
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
