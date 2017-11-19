using DataLayer.Repositories.Abstractions;
using DataLayer.DbContext;
using System.Collections.Generic;
using Common.Entities.OrganQueries;
using Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
    public class PatientOrganQueriesRepository : RepositoryBase<PatientOrganQuery>, IPatientOrganQueriesRepository
    {
        public PatientOrganQueriesRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.PatientOrganQueries)
        {
        }

        public IList<PatientOrganQuery> GetAllPending()
        {
            return GetAll(x => x.Status == PatientRequestStatuses.AwaitingForDonor);
        }

        public IList<PatientOrganQuery> GetPendingByOrganInfo(int organInfoId)
        {
            //TODO: use valud from enum
            return GetAll(x => x.OrganInfoId == organInfoId && x.Status == PatientRequestStatuses.AwaitingForDonor);
        }

        public PatientOrganQuery GetById(int patientOrganQueryId)
        {
            return GetSingleByPredicate(x => x.Id == patientOrganQueryId, x => x.Include(e => e.DonorOrganQuery));
        }
    }
}
