using BusinessLayer.Models.Enums;
using BusinessLayer.Models.ViewModels;
using DataLayer.Entities.OrganQueries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface IDonorOrganRequestService
    {
        IList<DonorOrganQuery> GetDonorRequestsByUsername(string userName);

        DonorOrganQuery GetById(int id);

        void RegisterDonorOrganRequest(DonorOrganRequestViewModel request);

        //TODO: maybe move to MedicalExamsService
        void ScheduleMedicalExam(ScheduleMedicalExamViewModel model);

        //TODO: maybe move to MedicalExamsService
        void UpdateMedicalExamResults(MedicalExamResultViewModel model);

        void ChangeStatusTo(int donorRequestId, DonorRequestStatuses status);
    }
}