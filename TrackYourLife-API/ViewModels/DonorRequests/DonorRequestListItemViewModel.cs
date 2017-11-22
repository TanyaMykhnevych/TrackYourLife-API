using System.Linq;
using BusinessLayer.Models.ViewModels.MedicalExam;
using Common.Entities.OrganRequests;
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

        public DonorRequestListItemViewModel(DonorRequest donorRequest)
        {
            Id = donorRequest.Id;
            Message = donorRequest.Message;
            Status = donorRequest.Status;
            DonorInfoId = donorRequest.DonorInfoId;
            OrganInfoId = donorRequest.OrganInfoId;
            OrganInfoName = donorRequest.OrganInfo.Name;
            HasLinkedPatientRequest = donorRequest.RequestsRelation != null
                && donorRequest.RequestsRelation.IsActive;
            MedicalExamsCount = donorRequest.DonorMedicalExams?.Count ?? 0;

            var lastMedExam = donorRequest.DonorMedicalExams?.LastOrDefault();
            if (lastMedExam != null)
            {
                LastMedicalExam = new DonorMedicalExamListItemViewModel(lastMedExam);
            }
        }
    }
}
