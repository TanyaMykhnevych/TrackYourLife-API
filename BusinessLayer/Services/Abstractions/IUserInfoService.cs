using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface IUserInfoService
    {
        Task<UserInfo> GetUserInfoByIdAsync(int id);

        Task<UserInfo> GetUserInfoByUserIdAsync(string id);
    }
}
