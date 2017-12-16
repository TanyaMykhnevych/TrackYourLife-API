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

        [HttpGet]
        public IActionResult GetOrganDeliverySnapshot(int id)
        {
            var response = ContentExecute(() =>
            {
                int patientRequestId = id;
                return _transportService.GetByPatientRequestId(patientRequestId);
            });

            return Json(response);
        }
    }
}
