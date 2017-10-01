using DataLayer.Entities.Organ;
using DataLayer.Entities.OrganQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class PatientQueueItem
    {
        public int Id { get; set; }

        // TODO: enum, from PatientOrganQuery
        public int Priority { get; set; }

        public int OrganInfoId { get; set; }
        public OrganInfo OrganInfo { get; set; }

        public int PatientUserInfoId { get; set; }
        public UserInfo PatientUserInfo { get; set; }

        public int PatientOrganQueryId { get; set; }
        public PatientOrganQuery PatientOrganQuery { get; set; }
    }
}
