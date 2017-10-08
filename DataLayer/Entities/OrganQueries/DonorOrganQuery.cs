using DataLayer.Entities.Base;
using DataLayer.Entities.Organ;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.OrganQueries
{
    public class DonorOrganQuery : BaseEntity
    {
        public int Id { get; set; }

        public string Message { get; set; }

        //TODO: Enum
        public int Status { get; set; }

        public int DonorInfoId { get; set; }
        public UserInfo DonorInfo { get; set; }

        public int OrganInfoId { get; set; }
        public OrganInfo OrganInfo { get; set; }

        public int? TransplantOrganId { get; set; }
        public TransplantOrgan TransplantOrgan { get; set; }
    }
}
