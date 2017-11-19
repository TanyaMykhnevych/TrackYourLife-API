using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.Enums
{
    public enum PatientRequestStatuses
    {
        AwaitingForDonor = 100,

        AwaitingForTransplanting = 200,

        FinishedSuccessfully = 300
    }
}
