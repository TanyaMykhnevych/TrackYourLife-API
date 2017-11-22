using BusinessLayer.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Models.ViewModels.Donor;
using Common.Entities.OrganRequests;
using Common.Enums;

namespace BusinessLayer.Services.Abstractions
{
    public interface IDonorRequestsService
    {
        IList<DonorRequest> GetDonorRequests();

        IList<DonorRequest> GetDonorRequestsByUsername(string userName);

        DonorRequest GetById(int id);

        DonorRequest GetDetailedById(int id);

        void FinishDonorRequestSuccessfully(int donorRequestId);

        void FinishDonorRequestFailed(int donorRequestId);

        bool HasDonorRequest(string id, int donorRequestId);

        void RegisterDonorOrganRequest(DonorRequestViewModel request);

        //TODO: maybe move to MedicalExamsService
        void ScheduleMedicalExam(ScheduleMedicalExamViewModel model);

        //TODO: maybe move to MedicalExamsService
        void UpdateMedicalExamResults(MedicalExamResultViewModel model);

        void ChangeStatusTo(int donorRequestId, DonorRequestStatuses status);
    }
}