using BusinessLayer.Models.ViewModels;
using System.Threading.Tasks;
using Common.Entities.OrganRequests;
using Common.Enums;
using System.Collections.Generic;

namespace BusinessLayer.Services.Abstractions
{
    public interface IPatientRequestsService
    {
        IList<PatientRequest> GetPatientRequests();

        IList<PatientRequest> GetPatientRequestsByUsername(string userName);

        PatientRequest GetById(int patientOrganRequestId);

        void AddPatientRequestToQueue(PatientOrganRequestViewModel patientOrganQueryViewModel);

        void ChangePatientRequestStatus(int patientOrganQueryId, PatientRequestStatuses status);
    }
}
