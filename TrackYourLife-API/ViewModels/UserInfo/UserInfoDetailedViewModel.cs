using System;

namespace TrackYourLife.API.ViewModels.UserInfo
{
    public class UserInfoDetailedViewModel
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Notes { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public UserInfoDetailedViewModel(global::Common.Entities.UserInfo userInfo)
        {
            this.Id = userInfo.UserInfoId;
            this.AppUserId = userInfo.AppUserId;
            this.Email = userInfo.Email;
            this.FirstName = userInfo.FirstName;
            this.SecondName = userInfo.SecondName;
            this.BirthDate = userInfo.BirthDate;
            this.Notes = userInfo.Notes;
            this.AddressLine1 = userInfo.AddressLine1;
            this.AddressLine2 = userInfo.AddressLine2;
            this.ZipCode = userInfo.ZipCode;
            this.Country = userInfo.Country;
            this.City = userInfo.City;
            this.PhoneNumber = userInfo.PhoneNumber;
        }
    }
}
