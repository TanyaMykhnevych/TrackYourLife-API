using BusinessLayer.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities;
using System.Threading.Tasks;
using DataLayer.Repositories.Abstractions;

namespace BusinessLayer.Services.Implementations
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public UserInfoService(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        public Task<UserInfo> GetUserInfoByIdAsync(int id)
        {
            return _userInfoRepository.GetSingleByAsync(x => x.UserInfoId == id);
        }

        public Task<UserInfo> GetUserInfoByUserIdAsync(string id)
        {
            return _userInfoRepository.GetSingleByAsync(x => x.AppUserId == id);
        }
    }
}
