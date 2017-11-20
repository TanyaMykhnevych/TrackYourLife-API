using Common.Entities.OrganQueries;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
    public class DonorOrganRequestRepository : RepositoryBase<DonorOrganQuery>, IDonorOrganRequestRepository
    {
        public DonorOrganRequestRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.DonorOrganQueries)
        {
        }

        public DonorOrganQuery GetById(int donorRequestId)
        {
            return GetSingleByPredicate(x => x.Id == donorRequestId);
        }

        public DonorOrganQuery GetDetailedById(int donorRequestId)
        {
            return GetSingleByPredicate(x => x.Id == donorRequestId,
                include: x => x.Include(dr => dr.DonorMedicalExams)
                    .Include(dr => dr.PatientOrganQuery)
                    .Include(dr => dr.DonorInfo)
                    .Include(dr => dr.OrganInfo)
                    .Include(dr => dr.TransplantOrgan));
        }

        //{
        //    var oldEntity = GetById(donorOrganRequest.Id);
        //    _appDbContext.Entry(oldEntity).State = EntityState.Detached;
    }
}
