using BusinessLayer.Models.ViewModels.Delivery;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TrackYourLife.API.Controllers
{
    public class OrganDeliveryController : Controller
    {
        private readonly IOrganTransportService _transportService;

        public OrganDeliveryController(IOrganTransportService transportService)
        {
            _transportService = transportService;
        }

        [HttpPost]
        public IActionResult ScheduleDelivery(ScheduleDeliveryViewModel model)
        {
            _transportService.ScheduleOrganDelivery(model);
            return Ok();
        }

        [HttpPost]
        public IActionResult AttachOrganDeliverySnapshot(OrganStateSnapshotViewModel model)
        {
            _transportService.AddOrganDeliverySnapshot(model);
            return Ok();
        }
    }
}
