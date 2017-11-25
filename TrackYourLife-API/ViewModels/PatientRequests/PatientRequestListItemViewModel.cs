using Common.Entities.OrganRequests;
using Common.Enums;

namespace TrackYourLife.API.ViewModels.PatientRequests
{
    public class PatientRequestListItemViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Message { get; set; }

        public PatientRequestStatuses Status { get; set; }

        public int? PatientInfoId { get; set; }

        public int OrganInfoId { get; set; }

        public string OrganInfoName { get; set; }
        
        public bool HasLinkedDonorRequest { get; set; }

        public PatientRequestPriority Priority { get; set; }

        public PatientRequestListItemViewModel(PatientRequest patientRequest)
        {
            Id = patientRequest.Id;
            Message = patientRequest.Message;
            Status = patientRequest.Status;
            FullName = patientRequest.PatientInfo.FirstName + " " + patientRequest.PatientInfo.SecondName;
            PatientInfoId = patientRequest.PatientInfoId;
            OrganInfoId = patientRequest.OrganInfoId;
            OrganInfoName = patientRequest.OrganInfo.Name;
            HasLinkedDonorRequest = patientRequest.RequestsRelation != null
                && patientRequest.RequestsRelation.IsActive;
            Priority = patientRequest.Priority;
        }
    }
}
