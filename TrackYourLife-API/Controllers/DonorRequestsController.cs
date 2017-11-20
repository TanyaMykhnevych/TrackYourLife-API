using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Models.ViewModels;
using BusinessLayer.Models.ViewModels.Donor;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Entities.Identity;
using Common.Enums;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrackYourLife.API.ViewModels.DonorRequests;

namespace TrackYourLife.API.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    public class DonorRequestsController : ControllerBase
    {
        private readonly IDonorOrganRequestService _donorRequestService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DonorRequestsController(IDonorOrganRequestService donorRequestService,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _donorRequestService = donorRequestService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Returns DonorRequestInfo by ID
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetDonorRequestDetails(int id)
        {
            int donorRequestId = id;
            var username = User.Identity.Name;
            var user = _userManager.FindByNameAsync(username).Result;
            bool isMedEmployee = _userManager.IsInRoleAsync(user, RolesConstants.MedicalEmployee).Result;
            bool hasRights = isMedEmployee;
            if (!hasRights)
            {
                hasRights = _donorRequestService.HasDonorRequest(user.Id, donorRequestId);
            }
            if (!hasRights)
            {
                return Unauthorized();
            }

            var response = ContentExecute<DonorRequestDetailsViewModel>(() =>
            {
                var donorRequest = _donorRequestService.GetDetailedById(donorRequestId);
                return new DonorRequestDetailsViewModel(donorRequest);
            });

            return Json(response);
        }

        /// <summary>
        ///  Creates Donor user account and send credentials by email
        ///  Saves DonorOrganQuery entity
        ///  Sends email to medical employee (about creating new request)
        /// </summary>
        [HttpPost]
        public IActionResult CreateDonorRequest(DonorRequestViewModel model)
        {
            var response = Execute(() =>
            {
                if (User != null && User.IsAuthenticated())
                {
                    if (User.IsInRole(RolesConstants.Donor))
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult ScheduleMedicalExam(ScheduleMedicalExamViewModel model)
        {
            var result = Execute(() =>
            {
                _donorRequestService.ScheduleMedicalExam(model);
            });

            return Json(result);
        }

        /// <summary>
        /// Creates TransplantOrgan entity (status ScheduledTransplanting...);
        /// Updates DonorMedicalExam entity;
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult SetMedicalExamResults(MedicalExamResultViewModel model)
        {
            var result = Execute(() =>
            {
                _donorRequestService.UpdateMedicalExamResults(model);
            });

            return Json(result);
        }

        /// <summary>
        /// Just changes donorOrganQuery status to AwaitingOrganRetrieving
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult TimeForRetrievingHasBeenScheduled(int id)
        {
            var result = Execute(() =>
            {
                int donorOrganQueryId = id;
                _donorRequestService.ChangeStatusTo(donorOrganQueryId, DonorRequestStatuses.AwaitingOrganRetrieving);
            });

            return Json(result);
        }

        /// <summary>
        /// Just changes donorOrganQuery status to FinishedSuccessfully
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult FinishDonorRequest(int id)
        {
            var result = Execute(() =>
            {
                int donorOrganQueryId = id;
                _donorRequestService.ChangeStatusTo(donorOrganQueryId, DonorRequestStatuses.FinishedSuccessfully);
            });

            return Json(result);
        }
    }
}