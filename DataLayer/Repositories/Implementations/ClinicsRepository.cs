using DataLayer.Repositories.Abstractions;
using DataLayer.DbContext;
using System.Linq;
using Common.Entities;

namespace DataLayer.Repositories.Implementations
{
    public class ClinicsRepository : RepositoryBase<Clinic>, IClinicsRepository
    {
        public ClinicsRepository(AppDbContext appDbContext) 
            : base(appDbContext, appDbContext.Clinics)
        {
        }

        public Clinic GetById(int id)
        {
            return GetSingleByPredicate(x => x.Id == id);
        }

        public Clinic GetFirst()
        {
            return DbSet.FirstOrDefault();
        }
    }
}
