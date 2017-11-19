using System.Collections.Generic;
using Common.Entities.Organ;
using Common.Entities.OrganQueries;
using Common.Enums;

namespace TrackYourLife.API.ViewModels.DonorRequests
{
    public class DonorRequestDetailsViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }
        
        public DonorRequestStatuses Status { get; set; }

        public int DonorInfoId { get; set; }

        public int OrganInfoId { get; set; }
        public OrganInfo OrganInfo { get; set; }

        public int? TransplantOrganId { get; set; }
        public TransplantOrgan TransplantOrgan { get; set; }

        public PatientOrganQuery PatientOrganQuery { get; set; }

        public ICollection<DonorMedicalExam> DonorMedicalExams { get; set; }

        public DonorRequestDetailsViewModel(DonorOrganQuery query)
        {
            Id = query.Id;
            Message = query.Message;
            Status = (DonorRequestStatuses)query.Status;
            DonorInfoId = query.DonorInfoId;
            OrganInfoId = query.OrganInfoId;
            OrganInfo = query.OrganInfo;
            TransplantOrganId = query.TransplantOrganId;
            TransplantOrgan = query.TransplantOrgan;
            PatientOrganQuery = query.PatientOrganQuery;
            DonorMedicalExams = query.DonorMedicalExams;
        }

        public DonorRequestDetailsViewModel() { }
    }
}
