using System;
using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Entities.Identity;
using Common.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TrackYourLife.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]/{id?}")]
    public class PatientRequestController : ControllerBase
    {
        private readonly IPatientRequestsService _patientOrganRequestService;
        private readonly IRequestsRelationsService _requestsRelationsService;
        private readonly UserManager<AppUser> _userManager;

        public PatientRequestController(
            IPatientRequestsService patientOrganRequestService,
            IRequestsRelationsService requestsRelationsService,
            UserManager<AppUser> userManager)
        {
            _patientOrganRequestService = patientOrganRequestService;
            _requestsRelationsService = requestsRelationsService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetPatientRequest(int id)
        {
            var result = ContentExecute(() =>
            {
                int patientRequestId = id;
                return _patientOrganRequestService.GetById(patientRequestId);
            });

            return Json(result);
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
                bool isMedEmployee = _userManager.IsUserInMedEmployeeRole(User.Identity.Name);
                if (!isMedEmployee)
                {
                    throw new UnauthorizedAccessException("You have not appropriate rights to access this action");
                }

                _patientOrganRequestService.AddPatientRequestToQueue(model);
            });

            return Json(result);
        }
        
        [HttpPost]
        public IActionResult LinkDonorRequest(PatientDonorRequestRelationViewModel model)
        {
            var result = Execute(() =>
            {
                bool isMedEmployee = _userManager.IsUserInMedEmployeeRole(User.Identity.Name);
                if (!isMedEmployee)
                {
                    throw new UnauthorizedAccessException("You have not appropriate rights to access this action");
                }

                _requestsRelationsService.CreatePatientDonorRequestsRelation(model.PatientRequestId, model.DonorRequestId);
            });

            return Json(result);
        }
    }
}
