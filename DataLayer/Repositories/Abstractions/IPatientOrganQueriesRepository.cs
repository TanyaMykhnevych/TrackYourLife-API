using System.Collections.Generic;
using Common.Entities.OrganQueries;

namespace DataLayer.Repositories.Abstractions
{
    public interface IPatientOrganQueriesRepository : IRepositoryBase<PatientOrganQuery>
    {
        IList<PatientOrganQuery> GetAllPending();

        IList<PatientOrganQuery> GetPendingByOrganInfo(int organInfoId);

        PatientOrganQuery GetById(int patientOrganQueryId);
    }
}
