using DataLayer.Entities.OrganQueries;

namespace DataLayer.Repositories.Abstractions
{
    public interface IMedicalExamsRepository
    {
        DonorMedicalExam GetById(int id);

        DonorMedicalExam Add(DonorMedicalExam entity);

        void Update(DonorMedicalExam entity);

        void Delete(int id);
    }
}
