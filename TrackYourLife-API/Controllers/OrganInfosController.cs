using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Common.Entities.Organ;
using TrackYourLife.API.ViewModels.OrganInfos;

namespace TrackYourLife.API.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrganInfosController : ControllerBase
    {
        private readonly IOrganInfoService _organInfoService;

        public OrganInfosController(IOrganInfoService organInfoService)
        {
            _organInfoService = organInfoService;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllOrganInfos()
        {
            var result = ContentExecute(() =>
            {
                return _organInfoService.GetOrganInfos();
            });

            return Json(result);
        }


        [HttpGet]
        public IActionResult GetOrganInfoList()
        {
            var result = ContentExecute<OrganInfoListViewModel>(() =>
            {
                var organInfos = _organInfoService.GetOrganInfos();
                var organInfoViewModels = organInfos.Select(o => new OrganInfoListItemViewModel(o)).ToList();
                return new OrganInfoListViewModel
                {
                    OrganInfoList = organInfoViewModels
                };
            });

            return Json(result);
        }

        [HttpGet]
        public IActionResult GetOrganInfoById(int id)
        {
            var result = ContentExecute(() =>
            {
                return _organInfoService.GetOrganInfoById(id);
            });

            return Json(result);
        }

        [HttpPost]
        public IActionResult AddOrganInfo(EditOrganInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: ne ok
            }

            var result = ContentExecute<OrganInfoDetailsViewModel>(() =>
            {
                var organInfo = new OrganInfo
                {
                    Name = model.Name,
                    Description = model.Description,
                    OutsideHumanPossibleTime = model.OutsideHumanPossibleTime
                };
                var newOrganInfo = _organInfoService.AddOrganInfo(organInfo);
                return new OrganInfoDetailsViewModel(newOrganInfo);
            });

            return Json(result);
        }

        [HttpPut]
        public IActionResult EditOrganInfo(EditOrganInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: ne ok
            }

            var result = ContentExecute<OrganInfoDetailsViewModel>(() =>
            {
                var organInfo = new OrganInfo
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    OutsideHumanPossibleTime = model.OutsideHumanPossibleTime
                };
                var editedOrganInfo = _organInfoService.UpdateOrganInfo(organInfo);
                return new OrganInfoDetailsViewModel(editedOrganInfo);
            });

            return Json(result);
        }
    }
}
