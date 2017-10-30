using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
    public enum AppClaimTypes
    {
        ViewDonorRequestList = 100,
        ViewDonorRequest = 200,
        EditDonorRequest = 300,
        ChangeDonorRequestStatus = 400,

        ViewOrganRequestList = 500,
        ViewOrganRequest = 600,
        EditOrganRequest = 700,
        CreateOrganRequest = 800,

        ConnectDonorAndOrganRequests = 1000,

        AddNewOrgan = 1500,
        AddNewMedicalEmployee = 1600,

        AddOrganInfo = 2000,
        RegisterNewPatient = 2100,
        AddClinic = 2200,

        ViewOrganTransportData = 2500
    }
}
