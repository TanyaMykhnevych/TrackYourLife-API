using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataLayer.Repositories.Implementations
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly AppDbContext _dbContext;

        public UserInfoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<UserInfo> GetSingleByAsync(Expression<Func<UserInfo, bool>> predicate)
        {
            return _dbContext.UserInfos.SingleOrDefaultAsync(predicate);
        }
    }
}
