using DataLayer.Entities;

namespace TrackYourLife.API.ViewModels.Clinics
{
    public class ClinicListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactPhone { get; set; }

        public string Country { get; set; }
        public string City { get; set; }

        public ClinicListItemViewModel(Clinic clinic)
        {
            Id = clinic.Id;
            Name = clinic.Name;
            ContactPhone = clinic.ContactPhone;
            Country = clinic.Country;
            City = clinic.City;
        }
    }
}
