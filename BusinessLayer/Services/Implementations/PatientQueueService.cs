using BusinessLayer.Services.Abstractions;
using DataLayer.Entities.OrganQueries;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Services.Implementations
{
    public class PatientQueueService : IPatientQueueService
    {
        private readonly IPatientOrganQueriesRepository _patientOrganQueriesRepository;
        private readonly IOrganInfoService _organInfoService;

        public PatientQueueService(
            IPatientOrganQueriesRepository patientOrganQueriesRepository,
            IOrganInfoService organInfoService)
        {
            _patientOrganQueriesRepository = patientOrganQueriesRepository;
            _organInfoService = organInfoService;
        }

        public IList<PatientOrganQuery> GetPengingQueueByOrgan(int organInfoId)
        {
            //TODO: add filters
            var patientQueries = _patientOrganQueriesRepository.GetPendingByOrganInfo(organInfoId);
            return SortQueue(patientQueries);
        }

        public IList<PatientOrganQuery> GetPengingQueue()
        {
            //TODO: add filters
            var patientQueries = _patientOrganQueriesRepository.GetAllPending();
            return SortQueue(patientQueries);
        }

        private IList<PatientOrganQuery> SortQueue(IList<PatientOrganQuery> organQueries)
        {
            return organQueries
                .OrderByDescending(x => x.Priority)
                .ThenBy(x => x.Id)
                .ToList();
        }
    }
}
