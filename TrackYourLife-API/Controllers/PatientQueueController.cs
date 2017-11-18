using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace TrackYourLife.API.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    public class PatientQueueController : ControllerBase
    {
        private readonly IPatientQueueService _patientQueueService;

        public PatientQueueController(
            IPatientQueueService patientQueueService)
        {
            _patientQueueService = patientQueueService;
        }

        public IActionResult GetPendingQueue()
        {
            var result = ContentExecute(() =>
            {
                var queue = _patientQueueService.GetPengingQueue();
                return queue;
            });

            return Json(result);
        }
        
        public IActionResult GetPendingQueueByOrgan(int id)
        {
            var result = ContentExecute(() =>
            {
                int organInfoId = id;
                var queue = _patientQueueService.GetPengingQueueByOrgan(organInfoId);
                return queue;
            });

            return Json(result);
        }
    }
}
