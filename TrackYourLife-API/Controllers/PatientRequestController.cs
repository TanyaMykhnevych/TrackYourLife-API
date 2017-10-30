using BusinessLayer.Models.ViewModels;
using BusinessLayer.Models.ViewModels.Patient;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TrackYourLife.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]/{id?}")]
    public class PatientRequestController : Controller
    {
        private readonly IPatientOrganRequestService _patientOrganRequestService;

        public PatientRequestController(
            IPatientOrganRequestService patientOrganRequestService)
        {
            _patientOrganRequestService = patientOrganRequestService;
        }

        [HttpGet]
        public IActionResult GetPatientRequest(int patientRequestId)
        {
            //TODO: maybe need to use ViewModel
            return Json(_patientOrganRequestService.GetById(patientRequestId));
        }

        /// <summary>
        /// Creates User/UserInfo for donor and sends email to donor
        /// Creates new PatientOrganQuery for patient
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreatePatientRequest(PatientOrganRequestViewModel model)
        {
            await _patientOrganRequestService.AddPatientOrganQueryToQueueAsync(model);
            return Ok();
        }

        /// <summary>
        /// Links DonorQuery to PatientQuery
        /// </summary>
        [HttpPost]
        public IActionResult AssignToDonorRequest(PatientToDonorViewModel model)
        {
            _patientOrganRequestService.AssignToDonorOrganQuery(model.PatientOrganQueryId, model.DonorOrganQueryId);
            return Ok();
        }
    }
}
