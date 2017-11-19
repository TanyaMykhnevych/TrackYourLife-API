using System;
using System.Linq;
using BusinessLayer.Models.ViewModels.Donor;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Entities;
using Common.Entities.Identity;
using Common.Utils;
using DataLayer.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.Services.Implementations
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly UserManager<AppUser> _userManager;

        public UserInfoService(IUserInfoRepository userInfoRepository,
            UserManager<AppUser> userManager)
        {
            _userInfoRepository = userInfoRepository;
            _userManager = userManager;
        }

        public UserInfo GetUserInfoById(int id)
        {
            return _userInfoRepository.GetSingleByPredicate(x => x.UserInfoId == id);
        }

        public UserInfo GetUserInfoByUserId(string id)
        {
            return _userInfoRepository.GetSingleByPredicate(x => x.AppUserId == id);
        }

        public UserInfo RegisterDonor(DonorRequestViewModel request)
        {
            var userInfo = new UserInfo()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                City = request.City,
                Country = request.Country,
                ZipCode = request.ZipCode,
                PhoneNumber = request.PhoneNumber,
                BirthDate = request.BirthDate
            };

            AppUser user = new AppUser()
            {
                Email = request.Email,
                UserName = request.Email,
                Created = DateTime.UtcNow,
                CreatedBy = CurrentUserHolder.GetCurrentUserName(),
                EmailConfirmed = true,
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed = true,
                UserInfo = userInfo
            };
            var result = _userManager.CreateAsync(user, request.Password).Result;
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, RolesConstants.Donor).Wait();
            }
            else
            {
                throw new ArgumentException(result.Errors.First().Description);
            }

            return userInfo;
        }
    }
}
