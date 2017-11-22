using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Models.ViewModels.MedicalExam;
using Common.Entities.OrganRequests;
using Common.Enums;
using TrackYourLife.API.ViewModels.Clinics;
using TrackYourLife.API.ViewModels.OrganInfos;
using TrackYourLife.API.ViewModels.PatientRequests;
using TrackYourLife.API.ViewModels.UserInfo;

namespace TrackYourLife.API.ViewModels.DonorRequests
{
    public class DonorRequestDetailsViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }
        
        public DonorRequestStatuses Status { get; set; }

        public int DonorInfoId { get; set; }
        public UserInfoDetailedViewModel DonorInfo { get; set; }

        public int OrganInfoId { get; set; }
        public OrganInfoDetailsViewModel OrganInfo { get; set; }
        
        public int? TransplantOrganId { get; set; }
        
        public PatientRequestDetailsViewModel PatientRequest { get; set; }

        public int MedicalExamsCount { get; set; }

        public DonorMedicalExamListItemViewModel LastDonorMedicalExam { get; set; }

        public ClinicListItemViewModel MedicalExamClinic { get; set; }

        public DonorRequestDetailsViewModel(DonorRequest request, PatientRequest patientRequest = null)
        {
            Id = request.Id;
            Message = request.Message;
            Status = request.Status;
            DonorInfoId = request.DonorInfoId;
            DonorInfo = new UserInfoDetailedViewModel(request.DonorInfo);
            OrganInfoId = request.OrganInfoId;
            OrganInfo = new OrganInfoDetailsViewModel(request.OrganInfo);
            TransplantOrganId = request.TransplantOrganId;
            MedicalExamsCount = request.DonorMedicalExams?.Count ?? 0;

            var lastExam = request.DonorMedicalExams?.LastOrDefault();
            if (lastExam != null)
            {
                LastDonorMedicalExam = new DonorMedicalExamListItemViewModel(lastExam);
                MedicalExamClinic = new ClinicListItemViewModel(lastExam.Clinic);
            }

            if (patientRequest != null && patientRequest.PatientInfo != null)
            {
                PatientRequest = new PatientRequestDetailsViewModel(patientRequest);
            }
        }

        public DonorRequestDetailsViewModel() { }
    }
}
