using BusinessLayer.Models.Enums;
using BusinessLayer.Models.ViewModels;
using DataLayer.Entities.Organ;
using DataLayer.Entities.OrganQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services.Abstractions
{
    public interface IDonorOrganRequestService
    {
        DonorOrganQuery GetById(int id);

        void RegisterDonorOrganRequest(DonorOrganRequestViewModel request);

        //TODO: maybe move to MedicalExamsService
        void ScheduleMedicalExam(ScheduleMedicalExamViewModel model);

        //TODO: maybe move to MedicalExamsService
        void UpdateMedicalExamResults(MedicalExamResultViewModel model);

        void ChangeStatusTo(int donorRequestId, DonorRequestStatuses status);
    }
}
