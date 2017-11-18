using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using DataLayer.Entities;

namespace DataLayer.Repositories.Implementations
{
    public class UserInfoRepository : RepositoryBase<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(AppDbContext dbContext)
            : base(dbContext, dbContext.UserInfos)
        {
        }
    }
}
