using System;

namespace BusinessLayer.Models.ViewModels
{
    public class ScheduleMedicalExamViewModel
    {
        public int DonorRequestId { get; set; }

        public int ClinicId { get; set; }

        public DateTime ScheduledDateTime { get; set; }
    }
}
