using BusinessLayer.Services.Abstractions;
using BusinessLayer.Models.ViewModels.Delivery;
using Common.Entities.OrganDelivery;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

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
                Humidity = model.Humidity,
                Time = DateTime.UtcNow,
                Longitude = model.Longitude,
                Altitude = model.Altitude
            };

            _deliverySnapshotsRepository.Add(snapshot);
        }

        public IList<OrganStateSnapshotViewModel> GetByPatientRequestId(int patientRequestId)
        {
            var patRequest = _patientRequestsService.GetDetailedById(patientRequestId);
            var donorRequest = patRequest.RequestsRelation.DonorRequest;
            var transplantOrganId = donorRequest.TransplantOrganId;
            if (!transplantOrganId.HasValue)
            {
                return Enumerable.Empty<OrganStateSnapshotViewModel>()
                    .ToList();
            } 

            var snapshots = _deliverySnapshotsRepository.GetByTransplantOrganId(transplantOrganId.Value)
                .TakeLast(10);

            return snapshots.Select(snapshot => new OrganStateSnapshotViewModel()
            {
                PatientRequestId = patientRequestId,
                Temperature = snapshot.Temperature,
                Humidity = snapshot.Humidity,
                Time = snapshot.Time,
                Longitude = snapshot.Longitude,
                Altitude = snapshot.Altitude
            }).ToList();
        }
    }
}
