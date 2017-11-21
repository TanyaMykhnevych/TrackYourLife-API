using System;
using BusinessLayer.Services.Abstractions;
using Common.Entities.OrganRequests;
using Common.Enums;
using DataLayer.Repositories.Abstractions;

namespace BusinessLayer.Services.Implementations
{
    public class RequestsRelationsService : IRequestsRelationsService
    {
        private readonly IPatientRequestsRepository _patientRequestsRepository;
        private readonly IDonorRequestsRepository _donorRequestsRepository;
        private readonly IRequestsRelationsRepository _requestsRelationsRepository;

        public RequestsRelationsService(
            IPatientRequestsRepository patientRequestsRepository,
            IDonorRequestsRepository donorRequestsRepository,
            IRequestsRelationsRepository requestsRelationsRepository)
        {
            _patientRequestsRepository = patientRequestsRepository;
            _donorRequestsRepository = donorRequestsRepository;
            _requestsRelationsRepository = requestsRelationsRepository;
        }

        public RequestsRelation GetByPatientDonorIds(int patientRequestId, int donorRequestId)
        {
            return _requestsRelationsRepository.GetSingleByPredicate(x =>
                x.PatientRequestId == patientRequestId && x.DonorRequestId == donorRequestId);
        }

        public void CreatePatientDonorRequestsRelation(int patientRequestId, int donorRequestId)
        {


            var patientRequest = _patientRequestsRepository.GetById(patientRequestId);
            if (patientRequest == null)
            {
                throw new ArgumentException(nameof(patientRequestId));
            }

            var donorRequest = _donorRequestsRepository.GetById(donorRequestId);
            if (donorRequest == null)
            {
                throw new ArgumentException(nameof(donorRequestId));
            }

            var relation = new RequestsRelation()
            {
                PatientRequestId = patientRequestId,
                DonorRequestId = donorRequestId,
                IsActive = true
            };

            patientRequest.Status = PatientRequestStatuses.AwaitingForTransplanting;
            donorRequest.Status = DonorRequestStatuses.NeedToScheduleTimeForOrganRetrieving;

            _donorRequestsRepository.Update(donorRequest);
            _patientRequestsRepository.Update(patientRequest);
            _requestsRelationsRepository.Add(relation);

            //TODO: send email to clinic/patient/donor that query status has been changed
        }
    }
}
