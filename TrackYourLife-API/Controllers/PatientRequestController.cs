using BusinessLayer.Models.ViewModels;
using BusinessLayer.Models.ViewModels.Patient;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TrackYourLife.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]/{id?}")]
    public class PatientRequestController : ControllerBase
    {
        private readonly IPatientOrganRequestService _patientOrganRequestService;

        public PatientRequestController(IPatientOrganRequestService patientOrganRequestService)
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
        public IActionResult CreatePatientRequest(PatientOrganRequestViewModel model)
        {
            var result = Execute(() =>
            {
                _patientOrganRequestService.AddPatientOrganQueryToQueue(model);
            });

            return Ok(result);
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
