using System;

namespace Common.Models
{
    public class EditPatientRequestModel
    {
        public int PatientRequestId { get; set; }

        public string PatientPhoneNumber { get; set; }

        public string PatientAddressLine1 { get; set; }

        public string Message { get; set; }
    }
}
