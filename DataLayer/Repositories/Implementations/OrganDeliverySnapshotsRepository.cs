using Common.Entities.OrganDelivery;
using Common.Utils;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories.Implementations
{
    public class OrganDeliverySnapshotsRepository : RepositoryBase<OrganDataSnapshot>, IOrganDeliverySnapshotsRepository
    {
        public OrganDeliverySnapshotsRepository(AppDbContext dbContext)
            : base(dbContext, dbContext.OrganDataSnapshots)
        {
        }

        public IList<OrganDataSnapshot> GetByTransplantOrganId(int transplantOrganId)
        {
            return GetAll(x => x.TransplantOrganId == transplantOrganId);
        }
    }
}
