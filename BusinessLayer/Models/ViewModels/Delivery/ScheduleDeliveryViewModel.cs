using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.ViewModels.Delivery
{
    //TODO: Must be reviewed!!!
    public class ScheduleDeliveryViewModel
    {
        public DateTime StartTransportTime { get; set; }

        public int FromClinicId { get; set; }

        public int ToClinicId { get; set; }

        public int DonorId { get; set; }

        public int PatientId { get; set; }

        public int PatientOrganRequestId { get; set; }
    }
}
