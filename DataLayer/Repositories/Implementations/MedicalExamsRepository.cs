using DataLayer.Repositories.Abstractions;
using DataLayer.Entities.OrganQueries;
using DataLayer.DbContext;

namespace DataLayer.Repositories.Implementations
{
    public class MedicalExamsRepository : RepositoryBase<DonorMedicalExam>, IMedicalExamsRepository
    {
        public MedicalExamsRepository(AppDbContext dbContext)
            : base(dbContext, dbContext.DonorMedicalExams)
        {
        }

        public DonorMedicalExam GetById(int id)
        {
            return GetSingleByPredicate(x => x.Id == id);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            Delete(entity);
        }
    }
}
