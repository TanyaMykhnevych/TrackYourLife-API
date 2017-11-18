using DataLayer.Entities.OrganQueries;
using System.Collections.Generic;

namespace DataLayer.Repositories.Abstractions
{
    public interface IPatientOrganQueriesRepository : IRepositoryBase<PatientOrganQuery>
    {
        IList<PatientOrganQuery> GetAllPending();

        IList<PatientOrganQuery> GetPendingByOrganInfo(int organInfoId);

        PatientOrganQuery GetById(int patientOrganQueryId);
    }
}
