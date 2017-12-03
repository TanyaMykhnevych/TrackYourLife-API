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
            var transplantOrgan = _transplantOrgansService.GetById(model.TransplantOrganId);
            if (transplantOrgan == null)
            {
                throw new ArgumentOutOfRangeException("Transplant organ ID is not exist.");
            }

            var deliveryInfo = transplantOrgan.OrganDeliveryInfo;
            if (!transplantOrgan.OrganDeliveryInfoId.HasValue)
            {
                deliveryInfo = _deliverySnapshotsRepository.CreateDeliveryInfo(transplantOrgan.Id);
            }

            var snapshot = new OrganDataSnapshot()
            {
                OrganDeliveryId = deliveryInfo.Id,
                Temperature = model.Temperature,
                Time = model.Time,
                Longitude = model.Longitude,
                Altitude = model.Altitude
            };

            _deliverySnapshotsRepository.Add(snapshot);
        }
    }
}
