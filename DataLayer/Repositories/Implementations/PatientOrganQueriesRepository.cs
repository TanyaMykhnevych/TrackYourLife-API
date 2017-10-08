using DataLayer.Repositories.Abstractions;
using DataLayer.Entities.OrganQueries;
using DataLayer.DbContext;
using System.Linq;

namespace DataLayer.Repositories.Implementations
{
    public class PatientOrganQueriesRepository : IPatientOrganQueriesRepository
    {
        private readonly AppDbContext _appDbContext;

        public PatientOrganQueriesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public PatientOrganQuery GetById(int patientOrganQueryId)
        {
            return _appDbContext.PatientOrganQueries.SingleOrDefault(x => x.Id == patientOrganQueryId);
        }

        public PatientOrganQuery Save(PatientOrganQuery patientOrganQuery)
        {
            patientOrganQuery = _appDbContext.PatientOrganQueries.Add(patientOrganQuery).Entity;
            _appDbContext.SaveChanges();

            return patientOrganQuery;
        }

        public void Update(PatientOrganQuery patientOrganQuery)
        {
            _appDbContext.PatientOrganQueries.Update(patientOrganQuery);
            _appDbContext.SaveChanges();
        }
    }
}
