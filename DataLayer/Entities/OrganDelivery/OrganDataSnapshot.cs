using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.OrganDelivery
{
    public class OrganDataSnapshot
    {
        public int Id { get; set; }

        public double Altitude { get; set; }
        public double Longitude { get; set; }

        public DateTime Time { get; set; }

        public float Temperature { get; set; }

        public int OrganDeliveryId { get; set; }
        public virtual OrganDeliveryInfo OrganDelivery { get; set; }
    }
}
