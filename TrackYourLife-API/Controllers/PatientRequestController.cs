using BusinessLayer.Models.ViewModels;
using BusinessLayer.Models.ViewModels.Patient;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackYourLife.API.Controllers
{
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
        public IActionResult CreatePatientRequest(PatientOrganRequestViewModel model)
        {
            _patientOrganRequestService.AddPatientOrganQueryToQueue(model);
            return Ok();
        }

        [HttpPost]
        public IActionResult AssignToDonorRequest(PatientToDonorViewModel model)
        {
            _patientOrganRequestService.AssignToDonorOrganQuery(model.PatientOrganQueryId, model.DonorOrganQueryId);
            return Ok();
        }
    }
}
