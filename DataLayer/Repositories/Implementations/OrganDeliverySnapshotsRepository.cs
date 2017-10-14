using DataLayer.DbContext;
using DataLayer.Entities.OrganDelivery;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories.Implementations
{
    public class OrganDeliverySnapshotsRepository : IOrganDeliverySnapshotsRepository
    {
        private readonly AppDbContext _dbContext;

        public OrganDeliverySnapshotsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OrganDataSnapshot Add(OrganDataSnapshot snapshot)
        {
            var entity = _dbContext.OrganDataSnapshots.Add(snapshot).Entity;
            _dbContext.SaveChanges();

            return entity;
        }
    }
}
