using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using System.Linq.Expressions;

namespace DataLayer.Repositories.Abstractions
{
    public interface IUserInfoRepository
    {
        Task<UserInfo> GetSingleByAsync(Expression<Func<UserInfo, bool>> predicate);
    }
}
