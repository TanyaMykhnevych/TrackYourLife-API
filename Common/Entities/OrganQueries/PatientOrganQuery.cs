using Common.Entities.Base;
using Common.Entities.Organ;
using Common.Enums;

namespace Common.Entities.OrganQueries
{
    public class PatientOrganQuery : BaseEntity
    {
        public int Id { get; set; }

        public string Message { get; set; }
        
        public PatientRequestStatuses Status { get; set; }
        
        public PatientQueryPriority Priority { get; set; }

        public int? PatientInfoId { get; set; }
        public UserInfo PatientInfo { get; set; }

        public int OrganInfoId { get; set; }
        public OrganInfo OrganInfo { get; set; }

        public DonorOrganQuery DonorOrganQuery { get; set; }
    }
}
