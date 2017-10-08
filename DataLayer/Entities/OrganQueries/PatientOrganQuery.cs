using DataLayer.Entities.Organ;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.OrganQueries
{
    public class PatientOrganQuery
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public int Status { get; set; }
        
        public int Priority { get; set; }

        public int? PatientInfoId { get; set; }
        public UserInfo PatientInfo { get; set; }

        public int OrganInfoId { get; set; }
        public OrganInfo OrganInfo { get; set; }

        public int? TransplantOrganId { get; set; }
        public TransplantOrgan TransplantOrgan { get; set; }
    }
}
