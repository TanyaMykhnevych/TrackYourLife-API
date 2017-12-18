using BusinessLayer.Models.ViewModels;
using System.Threading.Tasks;
using Common.Entities.OrganRequests;
using Common.Enums;
using System.Collections.Generic;
using Common.Models;

namespace BusinessLayer.Services.Abstractions
{
    public interface IPatientRequestsService
    {
        IList<PatientRequest> GetPatientRequests();

        IList<PatientRequest> GetReadyToTransportPatientRequests();

        IList<PatientRequest> GetPatientRequestsByUsername(string userName);

        PatientRequest GetById(int patientOrganRequestId);

        PatientRequest GetDetailedById(int id);

        bool HasPatientRequest(string id, int donorRequestId);

        void AddPatientRequestToQueue(PatientOrganRequestViewModel patientOrganQueryViewModel);

        void ChangePatientRequestStatus(int patientOrganQueryId, PatientRequestStatuses status);

        void UpdatePatientRequestWithPatient(EditPatientRequestModel model);
    }
}
