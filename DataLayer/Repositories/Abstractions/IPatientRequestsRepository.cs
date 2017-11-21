using System.Collections.Generic;
using Common.Entities.OrganRequests;

namespace DataLayer.Repositories.Abstractions
{
    public interface IPatientRequestsRepository : IRepositoryBase<PatientRequest>
    {
        IList<PatientRequest> GetAllPending();

        IList<PatientRequest> GetPendingByOrganInfo(int organInfoId);

        PatientRequest GetById(int patientOrganQueryId);
    }
}
