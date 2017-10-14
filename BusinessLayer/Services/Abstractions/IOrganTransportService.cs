using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Models.ViewModels.Delivery;

namespace BusinessLayer.Services.Abstractions
{
    public interface IOrganTransportService
    {
        void ScheduleOrganDelivery(ScheduleDeliveryViewModel model);
    }
}
