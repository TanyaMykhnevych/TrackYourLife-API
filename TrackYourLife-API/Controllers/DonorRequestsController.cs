using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Models.Enums;
using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackYourLife.API.ViewModels.DonorRequests;

namespace TrackYourLife.API.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    public class DonorRequestsController : ControllerBase
    {
        private readonly IDonorOrganRequestService _donorRequestService;

        public DonorRequestsController(IDonorOrganRequestService donorRequestService)
        {
            _donorRequestService = donorRequestService;
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
                var userName = User.Identity.Name;
                var donorRequests = _donorRequestService.GetDonorRequestsByUsername(userName);
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
            var response = ContentExecute<DonorRequestDetailsViewModel>(() =>
            {
                int donorRequestId = id;
                var userName = User.Identity.Name;
                var donorRequests = _donorRequestService.GetDonorRequestsByUsername(userName);
                if (!donorRequests.Any(x => x.Id == donorRequestId))
                {
                    return null;
                }
                var donorRequest = donorRequests.Single(x => x.Id == donorRequestId);
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
        public IActionResult CreateDonorRequest(DonorOrganRequestViewModel model)
        {
            //TODO: maybe need to convert viewmodel to DTO
            var response = Execute(() =>
            {
                _donorRequestService.RegisterDonorOrganRequest(model);
            });

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
