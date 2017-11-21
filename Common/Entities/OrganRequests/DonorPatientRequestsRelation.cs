using Common.Entities.Base;

namespace Common.Entities.OrganRequests
{
    public class DonorPatientRequestsRelation : BaseEntity
    {
        public int Id { get; set; }

        public int DonorRequestId { get; set; }

        public int PatientRequestId { get; set; }
    }
}
