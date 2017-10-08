using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.Enums
{
    public enum PatientQueryStatuses
    {
        AwaitingForDonor = 100,

        AwaitingForTransplanting = 200,

        FinishedSuccessfully = 300
    }
}
