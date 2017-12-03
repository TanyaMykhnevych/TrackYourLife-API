using System.Collections.Generic;
using Common.Entities.Base;
using Common.Entities.Organ;

namespace Common.Entities.OrganDelivery
{
    public class OrganDeliveryInfo : BaseEntity
    {
        public int Id { get; set; }
        
        // additional info will be here 

        public int TransplantOrganId { get; set; }
        public TransplantOrgan TransplantOrgan { get; set; }

        public virtual ICollection<OrganDataSnapshot> OrganDataSnapshots { get; set; }
    }
}
