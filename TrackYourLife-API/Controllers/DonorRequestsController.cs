using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Models.Enums;
using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using DataLayer.Entities.Identity;
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
            var user = _userManager.GetUserAsync(User).Result;
            var isMedEmployee = _userManager.IsInRoleAsync(user, RolesConstants.MedicalEmployee).Result;

            var response = ContentExecute(() =>
            {
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
            if (!HasRightToSeeDonorRequest(donorRequestId))
            {
                return Unauthorized();
            }

            var response = ContentExecute<DonorRequestDetailsViewModel>(() =>
            {
                var userName = User.Identity.Name;
                var donorRequests = _donorRequestService.GetDonorRequestsByUsername(userName);
                var donorRequest = donorRequests.SingleOrDefault(x => x.Id == donorRequestId);
                if (donorRequest == null)
                {
                    return null;
                }
                return new DonorRequestDetailsViewModel(donorRequest);
            });

            return Json(response);
        }

        private bool HasRightToSeeDonorRequest(int donorRequestId)
        {
            var user = _userManager.GetUserAsync(User).Result;
            bool hasRights = _userManager.IsInRoleAsync(user, RolesConstants.MedicalEmployee).Result;
            if (!hasRights)
            {
                hasRights = _donorRequestService.HasDonorRequest(user.Id, donorRequestId);
            }

            return hasRights;
        }

        /// <summary>
        ///  Creates Donor user account and send credentials by email
        ///  Saves DonorOrganQuery entity
        ///  Sends email to medical employee (about creating new request)
        /// </summary>
        [HttpPost]
        public IActionResult CreateDonorRequest(DonorOrganRequestViewModel model)
        {
            //TODO: maybe need to convert viewmodel to DTO
            var response = Execute(() => { _donorRequestService.RegisterDonorOrganRequest(model); });

            return Ok(response);
        }

        /// <summary>
        /// Creates DonorMedicalExam entity;
        /// Sends emails to Donor and to clinic;
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult ScheduleMedicalExam(ScheduleMedicalExamViewModel model)
        {
            var result = Execute(() => { _donorRequestService.ScheduleMedicalExam(model); });

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
            var result = Execute(() => { _donorRequestService.UpdateMedicalExamResults(model); });

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