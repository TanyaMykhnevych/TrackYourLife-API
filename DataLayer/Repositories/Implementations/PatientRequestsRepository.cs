using DataLayer.Repositories.Abstractions;
using DataLayer.DbContext;
using System.Collections.Generic;
using Common.Entities.OrganRequests;
using Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
    public class PatientRequestsRepository : RepositoryBase<PatientRequest>, IPatientRequestsRepository
    {
        public PatientRequestsRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.PatientRequests)
        {
        }

        public PatientRequest GetById(int patientOrganQueryId)
        {
            return GetSingleByPredicate(x => x.Id == patientOrganQueryId,
                 query => query.Include(e => e.RequestsRelation));
        }
    }
}
