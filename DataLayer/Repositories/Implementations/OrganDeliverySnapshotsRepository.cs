using DataLayer.DbContext;
using DataLayer.Entities.OrganDelivery;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories.Implementations
{
    public class OrganDeliverySnapshotsRepository : RepositoryBase<OrganDataSnapshot>, IOrganDeliverySnapshotsRepository
    {
        public OrganDeliverySnapshotsRepository(AppDbContext dbContext)
            : base(dbContext, dbContext.OrganDataSnapshots)
        {
        }
    }
}
