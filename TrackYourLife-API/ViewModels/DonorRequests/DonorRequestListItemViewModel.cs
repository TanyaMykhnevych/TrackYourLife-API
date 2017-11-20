using System.Linq;
using BusinessLayer.Models.ViewModels.MedicalExam;
using Common.Entities.OrganQueries;
using Common.Enums;

namespace TrackYourLife.API.ViewModels.DonorRequests
{
    public class DonorRequestListItemViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DonorRequestStatuses Status { get; set; }

        public int DonorInfoId { get; set; }

        public int OrganInfoId { get; set; }

        public string OrganInfoName { get; set; }

        public bool HasLinkedPatientRequest { get; set; }

        public int MedicalExamsCount { get; set; }

        public DonorMedicalExamListItemViewModel LastMedicalExam { get; set; }

        public DonorRequestListItemViewModel(DonorOrganQuery donorOrganQuery)
        {
            Id = donorOrganQuery.Id;
            Message = donorOrganQuery.Message;
            Status = donorOrganQuery.Status;
            DonorInfoId = donorOrganQuery.DonorInfoId;
            OrganInfoId = donorOrganQuery.OrganInfoId;
            OrganInfoName = donorOrganQuery.OrganInfo.Name;
            HasLinkedPatientRequest = donorOrganQuery.PatientOrganQuery != null;
            MedicalExamsCount = donorOrganQuery.DonorMedicalExams?.Count ?? 0;

            var lastMedExam = donorOrganQuery.DonorMedicalExams?.LastOrDefault();
            if (lastMedExam != null)
            {
                LastMedicalExam = new DonorMedicalExamListItemViewModel(lastMedExam);
            }
        }
    }
}
