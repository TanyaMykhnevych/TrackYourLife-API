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

        //TODO: need to implement enum
        public int Status { get; set; }

        // enum PatientQueryPriority
        public int Priority { get; set; }

        public int? PatientInfoId { get; set; }
        public UserInfo PatientInfo { get; set; }

        public int OrganInfoId { get; set; }
        public OrganInfo OrganInfo { get; set; }

        public DonorOrganQuery DonorOrganQuery { get; set; }
    }
}
