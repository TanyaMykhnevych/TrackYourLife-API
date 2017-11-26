using DataLayer.Repositories.Abstractions;
using DataLayer.DbContext;
using Common.Entities.OrganRequests;
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

        public PatientRequest GetDetailedById(int patientRequestId)
        {
            return GetSingleByPredicate(x => x.Id == patientRequestId,
                include:
                    x => x.Include(pt => pt.RequestsRelation)
                        .ThenInclude(dpr => dpr.DonorRequest)
                        .ThenInclude(pr => pr.DonorInfo)
                    .Include(pt => pt.OrganInfo)
                    .Include(pt => pt.PatientInfo));
        }
    }
}
