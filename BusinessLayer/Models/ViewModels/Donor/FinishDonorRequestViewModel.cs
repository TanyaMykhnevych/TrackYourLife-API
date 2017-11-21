using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.ViewModels.Donor
{
    public class FinishDonorRequestViewModel
    {
        public int DonorRequestId { get; set; }

        public DonorRequestStatuses DonorRequestStatus { get; set; }
    }
}
