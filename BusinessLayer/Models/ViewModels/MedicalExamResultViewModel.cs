using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.ViewModels
{
    public class MedicalExamResultViewModel
    {
        public int DonorOrganQueryId { get; set; }

        // TODO: create enum
        /// <summary>
        /// Pass, fail, retest, etc
        /// </summary>
        public int MedicalExamStatus { get; set; }

        public string MedicalExamResults { get; set; }
    }
}
