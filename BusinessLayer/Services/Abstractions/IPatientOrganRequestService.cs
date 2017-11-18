using BusinessLayer.Models.Enums;
using BusinessLayer.Models.ViewModels;
using DataLayer.Entities.Organ;
using DataLayer.Entities.OrganQueries;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface IPatientOrganRequestService
    {
        PatientOrganQuery GetById(int patientOrganRequestId);

        void AddPatientOrganQueryToQueue(PatientOrganRequestViewModel patientOrganQueryViewModel);

        void ChangePatientOrganQueryStatus(int patientOrganQueryId, PatientQueryStatuses status);

        void AssignToDonorOrganQuery(int patientOrganQueryId, int donorOrganQueryId);
    }
}
