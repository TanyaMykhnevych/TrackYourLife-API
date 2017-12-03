using Common.Entities.OrganDelivery;
using Common.Utils;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using System;

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
