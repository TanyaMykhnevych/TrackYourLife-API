using BusinessLayer.Services.Abstractions;
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
            var patientDonorRequestsLink = patientOrganRequest.RequestsRelation;
            var donorRequestId = patientDonorRequestsLink.DonorRequestId;
            var donorRequest = _donorRequestsService.GetDetailedById(donorRequestId);
            var donorOrgan = _transplantOrgansService.GetById(donorRequest.TransplantOrganId.Value);

            donorOrgan.OrganDeliveryInfo = deliveryInfo;
            
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
