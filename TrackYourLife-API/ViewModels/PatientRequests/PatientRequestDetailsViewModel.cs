using Common.Entities.Organ;
using Common.Entities.OrganRequests;
using Common.Enums;

namespace TrackYourLife.API.ViewModels.PatientRequests
{
    public class PatientRequestDetailsViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }
        
        public PatientRequestStatuses Status { get; set; }

        public int? PatientInfoId { get; set; }

        public int OrganInfoId { get; set; }
        public OrganInfo OrganInfo { get; set; }

        public DonorRequest DonorOrganQuery { get; set; }

        public PatientRequestDetailsViewModel(PatientRequest query, DonorRequest donorRequest)
        {
            Id = query.Id;
            Message = query.Message;
            Status = query.Status;
            PatientInfoId = query.PatientInfoId;
            OrganInfoId = query.OrganInfoId;
            OrganInfo = query.OrganInfo;
            DonorOrganQuery = donorRequest;
        }

        public PatientRequestDetailsViewModel() { }
    }
}
