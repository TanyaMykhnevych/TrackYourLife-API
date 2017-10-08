using DataLayer.Repositories.Abstractions;
using DataLayer.Entities;
using DataLayer.DbContext;
using System.Linq;

namespace DataLayer.Repositories.Implementations
{
    public class ClinicsRepository : IClinicsRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClinicsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Clinic GetFirst()
        {
            return _appDbContext.Clinics.FirstOrDefault();
        }
    }
}
