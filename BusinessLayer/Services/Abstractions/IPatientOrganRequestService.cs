using BusinessLayer.Models.Enums;
using DataLayer.Entities.Organ;
using DataLayer.Entities.OrganQueries;

namespace BusinessLayer.Services.Abstractions
{
    public interface IPatientOrganRequestService
    {
        PatientOrganQuery GetById(int patientOrganRequestId);

        void AddPatientOrganQueryToQueue(PatientOrganQuery patientOrganQuery);

        void ChangePatientOrganQueryStatus(int patientOrganQueryId, PatientQueryStatuses status);

        void AssignToDonorOrganQuery(int patientOrganQueryId, int donorOrganQueryId);
    }
}
