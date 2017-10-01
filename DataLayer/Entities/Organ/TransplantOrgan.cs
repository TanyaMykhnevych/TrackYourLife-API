using DataLayer.Entities.Base;
using DataLayer.Entities.OrganDelivery;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.Organ
{
    public class TransplantOrgan : BaseEntity
    {
        public int Id { get; set; }

        public int UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }

        public int OrganInfoId { get; set; }
        public OrganInfo OrganInfo { get; set; }

        public int? OrganDeliveryInfoId { get; set; }
        public virtual OrganDeliveryInfo OrganDeliveryInfo { get; set; }

        public int ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; }
    }
}
