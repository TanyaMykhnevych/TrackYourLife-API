using BusinessLayer.Models.ViewModels.Delivery;
using System.Collections.Generic;

namespace BusinessLayer.Services.Abstractions
{
    public interface IOrganTransportService
    {
        void AddOrganDeliverySnapshot(OrganStateSnapshotViewModel model);

        IList<OrganStateSnapshotViewModel> GetByPatientRequestId(int patientRequestId);
    }
}
