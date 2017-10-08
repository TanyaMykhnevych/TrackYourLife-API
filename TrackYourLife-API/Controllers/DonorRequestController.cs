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
    }
}
