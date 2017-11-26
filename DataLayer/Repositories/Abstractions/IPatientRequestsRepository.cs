using System.Collections.Generic;
using Common.Entities.OrganRequests;

namespace DataLayer.Repositories.Abstractions
{
    public interface IPatientRequestsRepository : IRepositoryBase<PatientRequest>
    {
        PatientRequest GetById(int patientOrganQueryId);

        PatientRequest GetDetailedById(int patientRequestId);
    }
}
