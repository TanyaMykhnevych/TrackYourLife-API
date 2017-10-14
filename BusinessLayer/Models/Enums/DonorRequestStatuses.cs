using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.Enums
{
    public enum DonorRequestStatuses
    {
        PendingMedicalExamination = 100,

        ScheduledMedicalExamination = 200,
        FailedMedicalExamination = 300,
        NeedToScheduleTimeForOrganRetrieving = 400,

        AwaitingOrganRetrieving = 500,

        FinishedSuccessfully = 600
    }
}
