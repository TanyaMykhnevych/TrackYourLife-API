using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.ViewModels.Donor
{
    public class DonorRequestViewModel
    {
        // Contacts
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public string PhoneNumber { get; set; }

        // what organ
        public int OrganInfoId { get; set; }

        public string Message { get; set; }
    }
}