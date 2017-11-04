using BusinessLayer.Services.Abstractions;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetClinics()
        {
            var result = await ContentExecuteAsync<ClinicListViewModel>(async () =>
            {
                var clinics = await _clinicsService.GetAllClinicsAsync();
                var clinicListItems = clinics.Select(x => new ClinicListItemViewModel(x));
                return new ClinicListViewModel()
                {
                    Clinics = clinicListItems.ToList()
                };
            });

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetClinicById(int id)
        {
            var result = await ContentExecuteAsync<Clinic>(async () =>
            {
                var clinic = await _clinicsService.GetClinicByIdAsync(id);
                return clinic;
            });

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddClinic([FromBody]EditClinicViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: ne ok
            }

            var result = await ContentExecuteAsync<ClinicDetailsViewModel>(async () =>
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
                var newClinic = await _clinicsService.AddClinicAsync(clinic);
                return new ClinicDetailsViewModel(newClinic);
            });

            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditClinic([FromBody]EditClinicViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: ne ok
            }

            var result = await ContentExecuteAsync<ClinicDetailsViewModel>(async () =>
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
                var newClinic = await _clinicsService.UpdateClinicAsync(clinic);
                return new ClinicDetailsViewModel(newClinic);
            });

            return Json(result);
        }
    }
}
