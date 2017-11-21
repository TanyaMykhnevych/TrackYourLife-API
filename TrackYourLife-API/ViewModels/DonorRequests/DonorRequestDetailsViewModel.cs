using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Models.ViewModels.MedicalExam;
using Common.Entities;
using Common.Entities.Organ;
using Common.Entities.OrganRequests;
using Common.Enums;

namespace TrackYourLife.API.ViewModels.DonorRequests
{
    public class DonorRequestDetailsViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }
        
        public DonorRequestStatuses Status { get; set; }

        public int DonorInfoId { get; set; }
        public UserInfo DonorInfo { get; set; }

        public int OrganInfoId { get; set; }
        public OrganInfo OrganInfo { get; set; }

        //TODO: replcae with viewmodel
        public int? TransplantOrganId { get; set; }
        public TransplantOrgan TransplantOrgan { get; set; }

        // TODO: replace with viewmodel
        public PatientRequest PatientOrganQuery { get; set; }

        public ICollection<DonorMedicalExamListItemViewModel> DonorMedicalExams { get; set; }

        public DonorRequestDetailsViewModel(DonorRequest query)
        {
            Id = query.Id;
            Message = query.Message;
            Status = query.Status;
            DonorInfoId = query.DonorInfoId;
            DonorInfo = query.DonorInfo;
            OrganInfoId = query.OrganInfoId;
            OrganInfo = query.OrganInfo;
            TransplantOrganId = query.TransplantOrganId;
            TransplantOrgan = query.TransplantOrgan;
            PatientOrganQuery = query.PatientRequest;
            DonorMedicalExams = query.DonorMedicalExams?.Select(x => new DonorMedicalExamListItemViewModel(x)).ToList();
        }

        public DonorRequestDetailsViewModel() { }
    }
}
