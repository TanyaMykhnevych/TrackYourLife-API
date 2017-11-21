using Common.Entities.OrganRequests;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories.Implementations
{
    public class RequestsRelationsRepository : RepositoryBase<RequestsRelation>, IRequestsRelationsRepository
    {
        public RequestsRelationsRepository(AppDbContext dbContext)
            : base(dbContext, dbContext.RequestsRelations)
        {
        }
    }
}
