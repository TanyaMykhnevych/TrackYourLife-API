using DataLayer.DbContext;
using DataLayer.Entities.OrganQueries;
using DataLayer.Repositories.Abstractions;

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
        
        //{
        //    var oldEntity = GetById(donorOrganRequest.Id);
        //    _appDbContext.Entry(oldEntity).State = EntityState.Detached;
    }
}
