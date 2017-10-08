using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackYourLife.API.Controllers
{
    public class PatientQueueController : Controller
    {
        private readonly IPatientQueueService _patientQueueService;

        public PatientQueueController(
            IPatientQueueService patientQueueService)
        {
            _patientQueueService = patientQueueService;
        }

        public IActionResult GetPendingQueue()
        {
            var queue = _patientQueueService.GetPengingQueue();
            return Json(queue);
        }

        public IActionResult GetPendingQueue(int organInfoId)
        {
            var queue = _patientQueueService.GetPengingQueueByOrgan(organInfoId);
            return Json(queue);
        }
    }
}
