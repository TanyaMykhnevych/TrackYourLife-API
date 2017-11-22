using Common.Entities.OrganRequests;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
    public class DonorRequestsRepository : RepositoryBase<DonorRequest>, IDonorRequestsRepository
    {
        public DonorRequestsRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.DonorRequests)
        {
        }

        public DonorRequest GetById(int donorRequestId)
        {
            return GetSingleByPredicate(x => x.Id == donorRequestId);
        }

        public DonorRequest GetDetailedById(int donorRequestId)
        {
            return GetSingleByPredicate(x => x.Id == donorRequestId,
                include: 
                    x => x.Include(dr => dr.DonorMedicalExams)
                        .ThenInclude(e => e.Clinic)
                    .Include(dr => dr.RequestsRelation)
                        .ThenInclude(dpr => dpr.PatientRequest)
                        .ThenInclude(pr => pr.PatientInfo)
                    .Include(dr => dr.OrganInfo)
                    .Include(dr => dr.DonorInfo)
                    .Include(dr => dr.TransplantOrgan));
        }
    }
}
