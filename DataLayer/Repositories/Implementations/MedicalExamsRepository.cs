using DataLayer.Repositories.Abstractions;
using DataLayer.Entities.OrganQueries;
using DataLayer.DbContext;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
    public class MedicalExamsRepository : IMedicalExamsRepository
    {
        private readonly AppDbContext _dbContext;

        public MedicalExamsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DonorMedicalExam GetById(int id)
        {
            return _dbContext.DonorMedicalExams.SingleOrDefault(x => x.Id == id);
        }

        public DonorMedicalExam Add(DonorMedicalExam entity)
        {
            var updatedEntity = _dbContext.DonorMedicalExams.Add(entity).Entity;
            _dbContext.SaveChanges();

            return updatedEntity;
        }

        public void Update(DonorMedicalExam entity)
        {
            var oldEntity = GetById(entity.Id);
            _dbContext.Entry(oldEntity).State = EntityState.Detached;

            entity.Created = oldEntity.Created;
            entity.CreatedBy = oldEntity.CreatedBy;

            _dbContext.DonorMedicalExams.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _dbContext.DonorMedicalExams.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
