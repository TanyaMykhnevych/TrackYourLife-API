using System.Linq;
using BusinessLayer.Models.Enums;
using DataLayer.Entities.OrganQueries;

namespace TrackYourLife.API.ViewModels.DonorRequests
{
    public class DonorRequestListItemViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }
        
        public DonorRequestStatuses Status { get; set; }

        public int DonorInfoId { get; set; }

        public int OrganInfoId { get; set; }

        public bool HasLinkedPatientRequest { get; set; }

        public int MedicalExamsCount { get; set; }

        public DonorMedicalExam LastMedicalExam { get; set; }

        public DonorRequestListItemViewModel(DonorOrganQuery donorOrganQuery)
        {
            Id = donorOrganQuery.Id;
            Message = donorOrganQuery.Message;
            Status = (DonorRequestStatuses)donorOrganQuery.Status;
            DonorInfoId = donorOrganQuery.DonorInfoId;
            OrganInfoId = donorOrganQuery.OrganInfoId;
            HasLinkedPatientRequest = donorOrganQuery.PatientOrganQuery != null;
            MedicalExamsCount = donorOrganQuery.DonorMedicalExams?.Count ?? 0;
            LastMedicalExam = donorOrganQuery.DonorMedicalExams?.LastOrDefault();
        }
    }
}
