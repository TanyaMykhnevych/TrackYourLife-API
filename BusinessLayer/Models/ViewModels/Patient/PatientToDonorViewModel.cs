using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.ViewModels.Patient
{
    public class PatientToDonorViewModel
    {
        public int PatientOrganQueryId { get; set; }

        public int DonorOrganQueryId { get; set; }
    }
}
