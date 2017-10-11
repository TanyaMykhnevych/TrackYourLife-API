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
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult CreatePatientRequest(PatientOrganRequestViewModel model)
        {
            //TODO: create patient account, send credentials by email
            //TODO: create PatientOrganQuery entity
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult AssignToDonorRequest(PatientToDonorViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
