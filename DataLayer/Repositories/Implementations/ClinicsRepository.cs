using DataLayer.Repositories.Abstractions;
using DataLayer.Entities;
using DataLayer.DbContext;
using System.Linq;

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
