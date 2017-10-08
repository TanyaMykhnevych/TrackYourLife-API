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
    }
}
