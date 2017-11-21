using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayer.Services.Abstractions;
using Common.Entities.OrganRequests;
using DataLayer.Migrations;
using DataLayer.Repositories.Abstractions;

namespace BusinessLayer.Services.Implementations
{
    public class MedicalExamsService : IMedicalExamsService
    {
        private readonly IMedicalExamsRepository _medicalExamsRepository;

        public MedicalExamsService(IMedicalExamsRepository medicalExamsRepository)
        {
            _medicalExamsRepository = medicalExamsRepository;
        }

        public IList<DonorMedicalExam> GetMedicalExams()
        {
            return _medicalExamsRepository.GetAll();
        }

        public IList<DonorMedicalExam> GetMedicalExamsByDonorRequestId(int donorRequestId)
        {
            return _medicalExamsRepository.GetAll(me => me.DonorRequestId == donorRequestId);
        }

        public DonorMedicalExam GetLastMedicalExamByDonorRequestId(int donorRequestId)
        {
            return _medicalExamsRepository.GetLastByPredicate(x => x.DonorRequestId == donorRequestId);
        }

        public DonorMedicalExam AddMedicalExam(DonorMedicalExam medicalExamEntity)
        {
            return _medicalExamsRepository.Add(medicalExamEntity);
        }

        public void UpdateMedicalExam(DonorMedicalExam exam)
        {
            _medicalExamsRepository.Update(exam);
        }
    }
}
