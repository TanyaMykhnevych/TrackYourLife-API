using BusinessLayer.Models.ViewModels.Delivery;

namespace BusinessLayer.Services.Abstractions
{
    public interface IOrganTransportService
    {
        void AddOrganDeliverySnapshot(OrganStateSnapshotViewModel model);
    }
}
