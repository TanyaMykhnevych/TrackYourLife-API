using System;
using System.Collections.Generic;
using System.Text;
using Common.Enums;

namespace BusinessLayer.Models.ViewModels
{
    public class MedicalExamResultViewModel
    {
        public int DonorRequestId { get; set; }
        
        public MedicalExamStatuses MedicalExamStatus { get; set; }

        public string MedicalExamResults { get; set; }
    }
}
