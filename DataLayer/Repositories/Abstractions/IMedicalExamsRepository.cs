using Common.Entities.OrganRequests;

namespace DataLayer.Repositories.Abstractions
{
    public interface IMedicalExamsRepository : IRepositoryBase<DonorMedicalExam>
    {
        DonorMedicalExam GetById(int id);

        void Delete(int id);
    }
}
