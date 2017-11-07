using BusinessLayer.Models.Enums;
using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        /// <param name="donorRequestId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetDonorRequestInfo(int donorRequestId)
        {
            // maybe need to use viewmodel
            return Json(_donorRequestService.GetById(donorRequestId));
        }

        /// <summary>
        ///  Creates Donor user account and send credentials by email
        ///  Saves DonorOrganQuery entity
        ///  Sends email to medical employee (about creating new request)
        /// </summary>
        [HttpPost]
        public IActionResult CreateDonorRequest([FromBody]DonorOrganRequestViewModel model)
        {
            //TODO: maybe need to convert viewmodel to DTO
            var response = this.Execute(() =>
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
        public IActionResult ScheduleMedicalExam([FromBody]ScheduleMedicalExamViewModel model)
        {
            _donorRequestService.ScheduleMedicalExam(model);
            return Ok();
        }

        /// <summary>
        /// Creates TransplantOrgan entity (status ScheduledTransplanting...);
        /// Updates DonorMedicalExam entity;
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult SetMedicalExamResults([FromBody]MedicalExamResultViewModel model)
        {
            _donorRequestService.UpdateMedicalExamResults(model);
            return Ok();
        }

        /// <summary>
        /// Just changes donorOrganQuery status to AwaitingOrganRetrieving
        /// </summary>
        /// <param name="donorOrganQueryId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult TimeForRetrievingHasBeenScheduled(int donorOrganQueryId)
        {
            _donorRequestService.ChangeStatusTo(donorOrganQueryId, DonorRequestStatuses.AwaitingOrganRetrieving);
            return Ok();
        }

        /// <summary>
        /// Just changes donorOrganQuery status to FinishedSuccessfully
        /// </summary>
        /// <param name="donorOrganQueryId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult FinishDonorRequest(int donorOrganQueryId)
        {
            _donorRequestService.ChangeStatusTo(donorOrganQueryId, DonorRequestStatuses.FinishedSuccessfully);
            return Ok();
        }
    }
}
