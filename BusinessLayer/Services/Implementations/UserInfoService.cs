using BusinessLayer.Services.Abstractions;
using DataLayer.Entities;
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

        public UserInfo GetUserInfoById(int id)
        {
            return _userInfoRepository.GetSingleByPredicate(x => x.UserInfoId == id);
        }

        public UserInfo GetUserInfoByUserId(string id)
        {
            return _userInfoRepository.GetSingleByPredicate(x => x.AppUserId == id);
        }
    }
}
