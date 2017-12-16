using System;
using Common.Entities.Base;

namespace Common.Entities.OrganDelivery
{
    public class OrganDataSnapshot : BaseEntity
    {
        public int Id { get; set; }

        public double Altitude { get; set; }
        public double Longitude { get; set; }

        public DateTime Time { get; set; }

        public float Temperature { get; set; }

        public float Humidity { get; set; }

        public int TransplantOrganId { get; set; }
    }
}
