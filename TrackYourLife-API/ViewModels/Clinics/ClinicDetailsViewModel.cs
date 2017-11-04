using DataLayer.Entities;

namespace TrackYourLife.API.ViewModels.Clinics
{
    public class ClinicDetailsViewModel
    {
        public ClinicDetailsViewModel(Clinic clinic)
        {
            Id = clinic.Id;
            Name = clinic.Name;
            ContactPhone = clinic.ContactPhone;
            ContactEmail = clinic.ContactEmail;
            Country = clinic.Country;
            City = clinic.City;
            AddressLine1 = clinic.AddressLine1;
            Longitude = clinic.Longitude;
            Altitude = clinic.Altitude;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string AddressLine1 { get; set; }

        public string Longitude { get; set; }
        public string Altitude { get; set; }
    }
}
