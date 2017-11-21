using Common.Entities.OrganRequests;
using Common.Enums;

namespace TrackYourLife.API.ViewModels.PatientRequests
{
    public class PatientRequestListItemViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }
        
        public PatientRequestStatuses Status { get; set; }

        public int? PatientInfoId { get; set; }

        public int OrganInfoId { get; set; }

        public string OrganInfoName { get; set; }
        
        public bool HasLinkedDonorRequest { get; set; }

        public PatientRequestListItemViewModel(PatientRequest patientOrganQuery)
        {
            Id = patientOrganQuery.Id;
            Message = patientOrganQuery.Message;
            Status = (PatientRequestStatuses)patientOrganQuery.Status;
            PatientInfoId = patientOrganQuery.PatientInfoId;
            OrganInfoId = patientOrganQuery.OrganInfoId;
            OrganInfoName = patientOrganQuery.OrganInfo.Name;
            HasLinkedDonorRequest = patientOrganQuery.DonorRequest != null;
        }
    }
}
