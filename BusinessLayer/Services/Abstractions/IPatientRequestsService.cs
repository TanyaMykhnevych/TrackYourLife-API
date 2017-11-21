using BusinessLayer.Models.ViewModels;
using System.Threading.Tasks;
using Common.Entities.OrganQueries;
using Common.Enums;

namespace BusinessLayer.Services.Abstractions
{
    public interface IPatientRequestsService
    {
        PatientRequest GetById(int patientOrganRequestId);

        void AddPatientOrganQueryToQueue(PatientOrganRequestViewModel patientOrganQueryViewModel);

        void ChangePatientOrganQueryStatus(int patientOrganQueryId, PatientRequestStatuses status);

        void AssignToDonorOrganQuery(int patientOrganQueryId, int donorOrganQueryId);
    }
}
