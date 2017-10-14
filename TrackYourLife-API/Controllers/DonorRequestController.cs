using BusinessLayer.Models.Enums;
using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace TrackYourLife.API.Controllers
{
    public class DonorRequestController : Controller
    {
        private readonly IDonorOrganRequestService _donorRequestService;

        public DonorRequestController(IDonorOrganRequestService donorRequestService)
        {
            _donorRequestService = donorRequestService;
        }
        /// <summary>
        /// Returns DonorRequestInfo by ID
        /// </summary>
        /// <param name="donorRequestId"></param>
        /// <returns></returns>
        [HttpGet]
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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateDonorRequest(DonorOrganRequestViewModel model)
        {
            //TODO: maybe need to convert viewmodel to DTO
            _donorRequestService.RegisterDonorOrganRequest(model);
            return Ok();
        }

        /// <summary>
        /// Creates DonorMedicalExam entity;
        /// Sends emails to Donor and to clinic;
        /// </summary>
        [HttpPost]
        public IActionResult ScheduleMedicalExam(ScheduleMedicalExamViewModel model)
        {
            _donorRequestService.ScheduleMedicalExam(model);
            return Ok();
        }

        /// <summary>
        /// Creates TransplantOrgan entity (status ScheduledTransplanting...);
        /// Updates DonorMedicalExam entity;
        /// </summary>
        [HttpPost]
        public IActionResult SetMedicalExamResults(MedicalExamResultViewModel model)
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
        public IActionResult FinishDonorRequest(int donorOrganQueryId)
        {
            _donorRequestService.ChangeStatusTo(donorOrganQueryId, DonorRequestStatuses.FinishedSuccessfully);
            return Ok();
        }
    }
}
