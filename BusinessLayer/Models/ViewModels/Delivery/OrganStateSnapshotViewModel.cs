using System;

namespace BusinessLayer.Models.ViewModels.Delivery
{
    public class OrganStateSnapshotViewModel
    {
        public int PatientRequestId { get; set; }

        public double Altitude { get; set; }
        public double Longitude { get; set; }

        public DateTime Time { get; set; }

        public float Temperature { get; set; }
    }
}
