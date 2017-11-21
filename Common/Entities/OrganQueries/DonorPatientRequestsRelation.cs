using System;
using System.Collections.Generic;
using System.Text;
using Common.Entities.Base;

namespace Common.Entities.OrganQueries
{
    public class DonorPatientRequestsRelation : BaseEntity
    {
        public int Id { get; set; }

        public int DonorRequestId { get; set; }

        public int PatientRequestId { get; set; }
    }
}
