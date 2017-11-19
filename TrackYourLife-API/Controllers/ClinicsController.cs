using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Common.Entities;
using TrackYourLife.API.ViewModels.Clinics;

namespace TrackYourLife.API.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClinicsController : ControllerBase
    {
        private readonly IClinicsService _clinicsService;

        public ClinicsController(IClinicsService clinicsService)
        {
            _clinicsService = clinicsService;
        }

        [HttpGet]
        public IActionResult GetClinics()
        {
            var result = ContentExecute<ClinicListViewModel>(() =>
            {
                var clinics = _clinicsService.GetAllClinics();
                var clinicListItems = clinics.Select(x => new ClinicListItemViewModel(x));
                return new ClinicListViewModel()
                {
                    Clinics = clinicListItems.ToList()
                };
            });

            return Json(result);
        }

        [HttpGet]
        public IActionResult GetClinicById(int id)
        {
            var result = ContentExecute<Clinic>(() =>
            {
                var clinic = _clinicsService.GetClinicById(id);
                return clinic;
            });

            return Json(result);
        }

        [HttpPost]
        public IActionResult AddClinic(EditClinicViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: ne ok
            }

            var result = ContentExecute<ClinicDetailsViewModel>(() =>
            {
                var clinic = new Clinic
                {
                    Name = model.Name,
                    ContactEmail = model.ContactEmail,
                    ContactPhone = model.ContactPhone,
                    Country = model.Country,
                    City = model.City,
                    AddressLine1 = model.AddressLine1,
                    Altitude = model.Altitude,
                    Longitude = model.Longitude
                };
                var newClinic = _clinicsService.AddClinic(clinic);
                return new ClinicDetailsViewModel(newClinic);
            });

            return Json(result);
        }

        [HttpPut]
        public IActionResult EditClinic(EditClinicViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: ne ok
            }

            var result = ContentExecute<ClinicDetailsViewModel>(() =>
            {
                var clinic = new Clinic
                {
                    Id = model.Id,
                    Name = model.Name,
                    ContactEmail = model.ContactEmail,
                    ContactPhone = model.ContactPhone,
                    Country = model.Country,
                    City = model.City,
                    AddressLine1 = model.AddressLine1,
                    Altitude = model.Altitude,
                    Longitude = model.Longitude
                };
                var newClinic = _clinicsService.UpdateClinic(clinic);
                return new ClinicDetailsViewModel(newClinic);
            });

            return Json(result);
        }
    }
}
