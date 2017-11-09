using BusinessLayer.Services.Abstractions;
using DataLayer.Entities.Organ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAllOrganInfos()
        {
            var result = await ContentExecuteAsync(async () =>
            {
                return await _organInfoService.GetOrganInfosAsync();
            });

            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetOrganInfoList()
        {
            var result = await ContentExecuteAsync<OrganInfoListViewModel>(async () =>
            {
                var organInfos = await _organInfoService.GetOrganInfosAsync();
                var organInfoViewModels = organInfos.Select(o => new OrganInfoListItemViewModel(o)).ToList();
                return new OrganInfoListViewModel
                {
                    OrganInfoList = organInfoViewModels
                };
            });

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganInfoById(int id)
        {
            var result = await ContentExecuteAsync(async () =>
            {
                return await _organInfoService.GetOrganInfoByIdAsync(id);
            });

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrganInfo([FromBody]EditOrganInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: ne ok
            }

            var result = await ContentExecuteAsync<OrganInfoDetailsViewModel>(async () =>
            {
                var organInfo = new OrganInfo
                {
                    Name = model.Name,
                    Description = model.Description,
                    OutsideHumanPossibleTime = model.OutsideHumanPossibleTime
                };
                var newOrganInfo = await _organInfoService.AddOrganInfoAsync(organInfo);
                return new OrganInfoDetailsViewModel(newOrganInfo);
            });

            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditOrganInfo([FromBody]EditOrganInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: ne ok
            }

            var result = await ContentExecuteAsync<OrganInfoDetailsViewModel>(async () =>
            {
                var organInfo = new OrganInfo
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    OutsideHumanPossibleTime = model.OutsideHumanPossibleTime
                };
                var editedOrganInfo = await _organInfoService.UpdateOrganInfoAsync(organInfo);
                return new OrganInfoDetailsViewModel(editedOrganInfo);
            });

            return Json(result);
        }
    }
}
