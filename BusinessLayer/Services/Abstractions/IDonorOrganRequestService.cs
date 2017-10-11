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

        void RegisterDonorOrganRequest(PatientOrganRequestViewModel request);

        void ChangeStatusTo(int donorRequestId, DonorRequestStatuses status);

        void ChangeStatusAndFillTransplantOrgan(int donorRequestId, TransplantOrgan transplantOrgan);
    }
}
