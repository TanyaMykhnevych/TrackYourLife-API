using BusinessLayer.Models.Enums;
using DataLayer.Entities.Organ;
using DataLayer.Entities.OrganQueries;

namespace BusinessLayer.Services.Abstractions
{
    public interface IPatientOrganRequestService
    {
        void AddPatientOrganQueryToQueue(PatientOrganQuery patientOrganQuery);

        void ChangePatientOrganQueryStatus(int patientOrganQueryId, PatientQueryStatuses status);

        void SetTransplantOrganToPatient(int patientOrganQueryId, TransplantOrgan transplantOrgan);
    }
}
