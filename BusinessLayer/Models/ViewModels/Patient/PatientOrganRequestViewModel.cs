using Common.Enums;

namespace BusinessLayer.Models.ViewModels
{
    public class PatientOrganRequestViewModel
    {
        public int ClinicId { get; set; }

        public int OrganInfoId { get; set; }

        public PatientRequestPriority QueryPriority { get; set; }

        public string AdditionalInfo { get; set; }

        // Contacts
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public string PhoneNumber { get; set; }
    }
}
