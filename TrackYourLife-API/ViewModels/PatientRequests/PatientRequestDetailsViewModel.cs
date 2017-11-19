using BusinessLayer.Models.Enums;
using DataLayer.Entities.Organ;
using DataLayer.Entities.OrganQueries;

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

        public DonorOrganQuery DonorOrganQuery { get; set; }

        public PatientRequestDetailsViewModel(PatientOrganQuery query)
        {
            Id = query.Id;
            Message = query.Message;
            Status = (PatientRequestStatuses)query.Status;
            PatientInfoId = query.PatientInfoId;
            OrganInfoId = query.OrganInfoId;
            OrganInfo = query.OrganInfo;
            DonorOrganQuery = query.DonorOrganQuery;
        }

        public PatientRequestDetailsViewModel() { }
    }
}
