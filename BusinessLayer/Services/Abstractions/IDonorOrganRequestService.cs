using BusinessLayer.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Models.ViewModels.Donor;
using Common.Entities.OrganQueries;
using Common.Enums;

namespace BusinessLayer.Services.Abstractions
{
    public interface IDonorOrganRequestService
    {
        IList<DonorOrganQuery> GetDonorRequests();

        IList<DonorOrganQuery> GetDonorRequestsByUsername(string userName);

        DonorOrganQuery GetById(int id);

        DonorOrganQuery GetDetailedById(int id);

        bool HasDonorRequest(string id, int donorRequestId);

        void RegisterDonorOrganRequest(DonorRequestViewModel request);

        //TODO: maybe move to MedicalExamsService
        void ScheduleMedicalExam(ScheduleMedicalExamViewModel model);

        //TODO: maybe move to MedicalExamsService
        void UpdateMedicalExamResults(MedicalExamResultViewModel model);

        void ChangeStatusTo(int donorRequestId, DonorRequestStatuses status);
    }
}