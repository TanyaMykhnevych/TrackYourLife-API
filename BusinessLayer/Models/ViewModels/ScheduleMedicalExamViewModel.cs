using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.ViewModels
{
    public class ScheduleMedicalExamViewModel
    {
        public int DonorOrganQueryId { get; set; }

        public int ClinicId { get; set; }

        public DateTime ScheduledDateTime { get; set; }
    }
}
