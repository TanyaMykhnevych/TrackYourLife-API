using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
