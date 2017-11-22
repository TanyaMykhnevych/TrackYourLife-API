using Common.Entities;

namespace TrackYourLife.API.ViewModels.Clinics
{
    public class ClinicListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactPhone { get; set; }
        public string AddressLine1 { get; set; }

        public string Country { get; set; }
        public string City { get; set; }

        public ClinicListItemViewModel(Clinic clinic)
        {
            Id = clinic.Id;
            Name = clinic.Name;
            ContactPhone = clinic.ContactPhone;
            AddressLine1 = clinic.AddressLine1;
            Country = clinic.Country;
            City = clinic.City;
        }
    }
}
