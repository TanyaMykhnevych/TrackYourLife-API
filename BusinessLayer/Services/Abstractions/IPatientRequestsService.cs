using BusinessLayer.Models.ViewModels;
using System.Threading.Tasks;
using Common.Entities.OrganRequests;
using Common.Enums;

namespace BusinessLayer.Services.Abstractions
{
    public interface IPatientRequestsService
    {
        PatientRequest GetById(int patientOrganRequestId);

        void AddPatientRequestToQueue(PatientOrganRequestViewModel patientOrganQueryViewModel);

        void ChangePatientRequestStatus(int patientOrganQueryId, PatientRequestStatuses status);
    }
}
