using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using DataLayer.Entities.OrganDelivery;

namespace DataLayer.Repositories.Implementations
{
    public class OrganDeliveryRepository : RepositoryBase<OrganDeliveryInfo>, IOrganDeliveryRepository
    {
        public OrganDeliveryRepository(AppDbContext dbContext)
            : base(dbContext, dbContext.OrganDeliveryInfos)
        {
        }
    }
}
