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

        public OrganDeliveryInfo CreateDeliveryInfo(int transplantOrganId)
        {
            var deliveryInfo = new OrganDeliveryInfo
            {
                TransplantOrganId = transplantOrganId, 
                Created = DateTime.UtcNow,
                CreatedBy = CurrentUserHolder.CurrentUser
            };

            var entry = DbContext.OrganDeliveryInfos.Add(deliveryInfo);
            DbContext.SaveChanges();

            return entry.Entity;
        }
    }
}
