using BusinessLayer.Models.ViewModels.Donor;
using Common.Entities;

namespace BusinessLayer.Services.Abstractions
{
    public interface IUserInfoService
    {
        UserInfo GetUserInfoById(int id);

        UserInfo GetUserInfoByUserId(string id);

        UserInfo RegisterDonor(DonorRequestViewModel request);
    }
}
