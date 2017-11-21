using Common.Entities.OrganQueries;
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
                include: x => x.Include(dr => dr.DonorMedicalExams)
                    .Include(dr => dr.PatientRequest)
                    .Include(dr => dr.DonorInfo)
                    .Include(dr => dr.OrganInfo)
                    .Include(dr => dr.TransplantOrgan));
        }

        //{
        //    var oldEntity = GetById(donorOrganRequest.Id);
        //    _appDbContext.Entry(oldEntity).State = EntityState.Detached;
    }
}
