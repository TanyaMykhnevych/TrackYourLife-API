using Common.Entities.OrganQueries;

namespace DataLayer.Repositories.Abstractions
{
    public interface IMedicalExamsRepository : IRepositoryBase<DonorMedicalExam>
    {
        DonorMedicalExam GetById(int id);

        void Delete(int id);
    }
}
