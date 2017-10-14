using DataLayer.Repositories.Abstractions;
using DataLayer.Entities.OrganQueries;
using DataLayer.DbContext;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
    public class PatientOrganQueriesRepository : IPatientOrganQueriesRepository
    {
        private readonly AppDbContext _appDbContext;

        public PatientOrganQueriesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IList<PatientOrganQuery> GetAllPending()
        {
            return _appDbContext
                .PatientOrganQueries
                //TODO: use valud from enum
                .Where(x => x.Status == 100)
                .ToList();
        }

        public IList<PatientOrganQuery> GetPendingByOrganInfo(int organInfoId)
        {
            return _appDbContext
                .PatientOrganQueries
                //TODO: use valud from enum
                .Where(x => x.OrganInfoId == organInfoId && x.Status == 100)
                .ToList();
        }

        public PatientOrganQuery GetById(int patientOrganQueryId)
        {
            return _appDbContext
                .PatientOrganQueries
                .Include(x => x.DonorOrganQuery)
                .SingleOrDefault(x => x.Id == patientOrganQueryId);
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
