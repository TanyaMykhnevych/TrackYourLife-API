using Common.Entities.Organ;
using Common.Entities.OrganRequests;
using Common.Enums;
using TrackYourLife.API.ViewModels.DonorRequests;
using TrackYourLife.API.ViewModels.OrganInfos;
using TrackYourLife.API.ViewModels.UserInfo;

namespace TrackYourLife.API.ViewModels.PatientRequests
{
    public class PatientRequestDetailsViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }
        
        public PatientRequestStatuses Status { get; set; }

        public int? PatientInfoId { get; set; }
        public UserInfoDetailedViewModel PatientInfo { get; set; }

        public int OrganInfoId { get; set; }
        public OrganInfoDetailsViewModel OrganInfo { get; set; }

        public DonorRequestDetailsViewModel DonorRequest { get; set; }

        public PatientRequestDetailsViewModel(PatientRequest request, DonorRequest donorRequest = null)
        {
            Id = request.Id;
            Message = request.Message;
            Status = request.Status;
            PatientInfoId = request.PatientInfoId;
            PatientInfo = new UserInfoDetailedViewModel(request.PatientInfo);
            OrganInfoId = request.OrganInfoId;
            OrganInfo = new OrganInfoDetailsViewModel(request.OrganInfo);

            if (donorRequest != null)
            {
                DonorRequest = new DonorRequestDetailsViewModel(donorRequest);
            }
        }

        public PatientRequestDetailsViewModel() { }
    }
}
