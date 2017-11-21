using BusinessLayer.Services.Abstractions;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Entities.OrganRequests;
using Common.Enums;

namespace BusinessLayer.Services.Implementations
{
    public class PatientQueueService : IPatientQueueService
    {
        private readonly IPatientRequestsRepository _patientRequestsRepository;

        public PatientQueueService(IPatientRequestsRepository patientRequestsRepository)
        {
            _patientRequestsRepository = patientRequestsRepository;
        }

        public IList<PatientRequest> GetPengingQueue()
        {
            //TODO: add filters
            var patientQueries = _patientRequestsRepository.GetAll(x => x.Status == PatientRequestStatuses.AwaitingForDonor);
            return SortQueue(patientQueries);
        }

        public IList<PatientRequest> GetPengingQueueByOrgan(int organInfoId)
        {
            //TODO: add filters
            var patientQueries = _patientRequestsRepository.GetAll(x =>
                x.OrganInfoId == organInfoId && x.Status == PatientRequestStatuses.AwaitingForDonor);

            return SortQueue(patientQueries);
        }

        private IList<PatientRequest> SortQueue(IList<PatientRequest> organQueries)
        {
            return organQueries
                .OrderByDescending(x => x.Priority)
                .ThenBy(x => x.Id)
                .ToList();
        }
    }
}