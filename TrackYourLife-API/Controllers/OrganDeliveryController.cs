using BusinessLayer.Models.ViewModels.Delivery;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace TrackYourLife.API.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    public class OrganDeliveryController : ControllerBase
    {
        private readonly IOrganTransportService _transportService;

        public OrganDeliveryController(IOrganTransportService transportService)
        {
            _transportService = transportService;
        }

        /// <summary>
        /// Creates OrganDeliveryInfo and links it to TransplantOrgan 
        /// (to TransplantOrgan of DonorOrganRequest which assigned to current PatientOrganRequest)
        /// </summary>
        [HttpPost]
        public IActionResult ScheduleDelivery(ScheduleDeliveryViewModel model)
        {
            var result = this.Execute(() =>
            {
                _transportService.ScheduleOrganDelivery(model);
            });

            return Json(result);
        }

        /// <summary>
        /// Adds new delivery snapshot to OrganDeliveryInfo
        /// </summary>
        [HttpPost]
        public IActionResult AttachOrganDeliverySnapshot(OrganStateSnapshotViewModel model)
        {
            var result = this.Execute(() =>
            {
                _transportService.AddOrganDeliverySnapshot(model);
            });

            return Json(result);
        }
    }
}
