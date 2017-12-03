using BusinessLayer.Services.Abstractions;
using BusinessLayer.Models.ViewModels.Delivery;
using Common.Entities.OrganDelivery;
using DataLayer.Repositories.Abstractions;
using System;

namespace BusinessLayer.Services.Implementations
{
    public class OrganTransportService : IOrganTransportService
    {
        private readonly IOrganDeliverySnapshotsRepository _deliverySnapshotsRepository;
        private readonly ITransplantOrgansService _transplantOrgansService;
        private readonly IPatientRequestsService _patientRequestsService;
        private readonly IDonorRequestsService _donorRequestsService;

        public OrganTransportService(
            IOrganDeliverySnapshotsRepository deliverySnapshotsRepository,
            ITransplantOrgansService transplantOrgansService,
            IPatientRequestsService patientRequestsService,
            IDonorRequestsService donorRequestsService)
        {
            _deliverySnapshotsRepository = deliverySnapshotsRepository;
            _transplantOrgansService = transplantOrgansService;
            _patientRequestsService = patientRequestsService;
            _donorRequestsService = donorRequestsService;
        }

        public void AddOrganDeliverySnapshot(OrganStateSnapshotViewModel model)
        {
            var patientRequest = _patientRequestsService.GetById(model.PatientRequestId);
            if (patientRequest == null)
            {
                throw new ArgumentOutOfRangeException("PatientRequest is not exist.");
            }

            var donorRequest = _donorRequestsService.GetById(patientRequest.RequestsRelation.DonorRequestId);
            if (donorRequest == null)
            {
                throw new ArgumentOutOfRangeException("DonorRequest is not exist.");
            }
            if (!donorRequest.TransplantOrganId.HasValue)
            {
                throw new ArgumentOutOfRangeException("DonorRequest has not linked transplant organ.");
            }

            var transplantOrgan = _transplantOrgansService.GetById(donorRequest.TransplantOrganId.Value);
            if (transplantOrgan == null)
            {
                throw new ArgumentOutOfRangeException("Transplant organ is not exist.");
            }

            var snapshot = new OrganDataSnapshot()
            {
                TransplantOrganId = transplantOrgan.Id,
                Temperature = model.Temperature,
                Time = model.Time,
                Longitude = model.Longitude,
                Altitude = model.Altitude
            };

            _deliverySnapshotsRepository.Add(snapshot);
        }
    }
}
