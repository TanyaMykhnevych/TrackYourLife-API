using BusinessLayer.Models.ViewModels;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackYourLife.API.Controllers
{
    public class DonorRequestController : Controller
    {
        private readonly IDonorOrganRequestService _donorRequestService;

        public DonorRequestController(IDonorOrganRequestService donorRequestService)
        {
            _donorRequestService = donorRequestService;
        }

        [HttpGet]
        public IActionResult GetDonorRequestInfo(int donorRequestId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult CreateDonorRequest(DonorOrganRequestViewModel model)
        {
            // TODO: Create Donor user account and send credentials by email
            // TODO: Save DonorOrganQuery entity
            // TODO: send email to medical employee (about new request)
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult ScheduleMedicalExam(ScheduleMedicalExamViewModel model)
        {
            //TODO: create DonorMedicalExam entity
            //TODO: send emails to Donor and to clinic
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult SetMedicalExamResults(MedicalExamResultViewModel model)
        {
            //TODO: create TransplantOrgan entity (status ScheduledTransplanting...),
            //TODO: update DonorMedicalExam entity
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult FinishDonorRequest(MedicalExamResultViewModel model)
        {
            //TODO: update DonorOrganQuery status to 'Finished'
            throw new NotImplementedException();
        }
    }
}
