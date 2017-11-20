namespace Common.Enums
{
    public enum DonorRequestStatuses
    {
        PendingMedicalExamination = 100,

        ScheduledMedicalExamination = 200,
        FailedMedicalExamination = 300,

        AwaitingForPatientRequest = 350,

        NeedToScheduleTimeForOrganRetrieving = 400,

        AwaitingOrganRetrieving = 500,

        FinishedSuccessfully = 600
    }
}
