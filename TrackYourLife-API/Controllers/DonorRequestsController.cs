using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Models.ViewModels;
using BusinessLayer.Models.ViewModels.Donor;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Entities.Identity;
using Common.Enums;
using Common.Utils;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrackYourLife.API.ViewModels.DonorRequests;

namespace TrackYourLife.API.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DonorRequestsController : ControllerBase
    {
        private readonly IDonorRequestsService _donorRequestService;
        private readonly IPatientQueueService _patientQueueService;
        private readonly IRequestsRelationsService _requestsRelationsService;
        private readonly UserManager<AppUser> _userManager;

        public DonorRequestsController(
            IDonorRequestsService donorRequestService,
            IRequestsRelationsService requestsRelationsService,
            IPatientQueueService patientQueueService,
            UserManager<AppUser> userManager)
        {
            _donorRequestService = donorRequestService;
            _requestsRelationsService = requestsRelationsService;
            _patientQueueService = patientQueueService;
            _userManager = userManager;
        }

        /// <summary>
        /// Returns DonorRequestInfo by ID
        /// </summary>
        [HttpGet]
        public IActionResult GetDonorRequestInfo(int id)
        {
            var response = ContentExecute(() =>
            {
                int donorRequestId = id;
                return _donorRequestService.GetById(donorRequestId);
            });

            return Json(response);
        }


        /// <summary>
        /// Returns Donor Requests List
        /// </summary>
        [HttpGet]
        public IActionResult GetDonorRequestList()
        {
            var response = ContentExecute(() =>
            {
                var username = User.Identity.Name;
                var user = _userManager.FindByNameAsync(username).Result;
                var isMedEmployee = _userManager.IsInRoleAsync(user, RolesConstants.MedicalEmployee).Result;
                var donorRequests = isMedEmployee
                    ? _donorRequestService.GetDonorRequests()
                    : _donorRequestService.GetDonorRequestsByUsername(user.UserName);

                var donorRequestListItems = donorRequests.Select(dr => new DonorRequestListItemViewModel(dr)).ToList();
                return new DonorRequestListViewModel(donorRequestListItems);
            });

            return Json(response);
        }

        /// <summary>
        /// Returns Donor Requests List
        /// </summary>
        [HttpGet]
        public IActionResult GetDonorRequestDetails(int id)
        {
            int donorRequestId = id;
            bool hasRights = _userManager.IsUserInMedEmployeeRole(User.Identity.Name);
            if (!hasRights)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                hasRights = _donorRequestService.HasDonorRequest(user.Id, donorRequestId);
            }
            if (!hasRights)
            {
                return Unauthorized();
            }

            var response = ContentExecute<DonorRequestDetailsViewModel>(() =>
            {
                var donorRequest = _donorRequestService.GetDetailedById(donorRequestId);
                return new DonorRequestDetailsViewModel(donorRequest, donorRequest.RequestsRelation?.PatientRequest);
            });

            return Json(response);
        }

        /// <summary>
        ///  Creates Donor user account and send credentials by email
        ///  Saves DonorOrganQuery entity
        ///  Sends email to medical employee (about creating new request)
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateDonorRequest(DonorRequestViewModel model)
        {
            var response = Execute(() =>
            {
                if (User != null && User.IsAuthenticated())
                {
                    if (_userManager.IsUserInDonorRole(User.Identity.Name))
                    {
                        var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                        model.Email = user.Email;
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("Cannot access to this method because you are not a donor!");
                    }
                }

                _donorRequestService.RegisterDonorOrganRequest(model);
            });

            return Json(response);
        }

        /// <summary>
        /// Creates DonorMedicalExam entity;
        /// Sends emails to Donor and to clinic;
        /// </summary>
        [HttpPost]
        public IActionResult ScheduleMedicalExam(ScheduleMedicalExamViewModel model)
        {
            var result = Execute(() =>
            {
                bool isMedEmployee = _userManager.IsUserInMedEmployeeRole(User.Identity.Name);
                if (!isMedEmployee)
                {
                    throw new UnauthorizedAccessException("You have not appropriate rights to access this action");
                }

                _donorRequestService.ScheduleMedicalExam(model);
            });

            return Json(result);
        }

        /// <summary>
        /// Creates TransplantOrgan entity (status ScheduledTransplanting...);
        /// Updates DonorMedicalExam entity;
        /// </summary>
        [HttpPost]
        public IActionResult SetMedicalExamResults(MedicalExamResultViewModel model)
        {
            var result = Execute(() =>
            {
                bool isMedEmployee = _userManager.IsUserInMedEmployeeRole(User.Identity.Name);
                if (!isMedEmployee)
                {
                    throw new UnauthorizedAccessException("You have not appropriate rights to access this action");
                }

                _donorRequestService.UpdateMedicalExamResults(model);
            });

            return Json(result);
        }

        [HttpPost]
        public IActionResult LinkPatientRequest(PatientDonorRequestRelationViewModel model)
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

        /// <summary>
        /// Just changes donorOrganQuery status to AwaitingOrganRetrieving
        /// </summary>
        [HttpPost]
        public IActionResult TimeForRetrievingHasBeenScheduled([FromRoute]int id)
        {
            var result = Execute(() =>
            {
                bool isMedEmployee = _userManager.IsUserInMedEmployeeRole(User.Identity.Name);
                if (!isMedEmployee)
                {
                    throw new UnauthorizedAccessException("You have not appropriate rights to access this action");
                }

                int donorOrganQueryId = id;
                _donorRequestService.ChangeStatusTo(donorOrganQueryId, DonorRequestStatuses.AwaitingOrganRetrieving);
            });

            return Json(result);
        }

        /// <summary>
        /// Changes donorOrganQuery status to FinishedSuccessfully
        /// </summary>
        [HttpPost]
        public IActionResult FinishDonorRequest(FinishDonorRequestViewModel model)
        {
            var result = Execute(() =>
            {
                bool isMedEmployee = _userManager.IsUserInMedEmployeeRole(User.Identity.Name);
                if (!isMedEmployee)
                {
                    throw new UnauthorizedAccessException("You have not appropriate rights to access this action");
                }

                if (model.DonorRequestStatus != DonorRequestStatuses.FinishedFailed
                && model.DonorRequestStatus != DonorRequestStatuses.FinishedSuccessfully)
                {
                    throw new UnauthorizedAccessException("This method must be used only for setting Success or Failed to DonorRequest status.");
                }

                if (model.DonorRequestStatus == DonorRequestStatuses.FinishedSuccessfully)
                {
                    _donorRequestService.FinishDonorRequestSuccessfully(model.DonorRequestId);
                }
                else
                {
                    _donorRequestService.FinishDonorRequestFailed(model.DonorRequestId);
                }
            });

            return Json(result);
        }
    }
}