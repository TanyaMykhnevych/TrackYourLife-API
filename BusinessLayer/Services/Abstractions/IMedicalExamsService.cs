using System;
using System.Collections.Generic;
using System.Text;
using Common.Entities.OrganRequests;

namespace BusinessLayer.Services.Abstractions
{
    public interface IMedicalExamsService
    {
        IList<DonorMedicalExam> GetMedicalExams();

        IList<DonorMedicalExam> GetMedicalExamsByDonorRequestId(int donorRequestId);

        DonorMedicalExam GetLastMedicalExamByDonorRequestId(int donorRequestId);

        DonorMedicalExam AddMedicalExam(DonorMedicalExam medicalExamEntity);

        void UpdateMedicalExam(DonorMedicalExam exam);
    }
}
