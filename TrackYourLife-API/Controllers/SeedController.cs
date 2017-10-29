using DataLayer.DbContext;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackYourLife.API.Controllers
{
    [Route("[controller]")]
    public class SeedController : Controller
    {
        private readonly IDbInitializer _dbInitializer;

        public SeedController(IDbInitializer dbInitializer)
        {
            _dbInitializer = dbInitializer;
        }

        [HttpGet]
        public IActionResult Start()
        {
            _dbInitializer.Initialize();
            return Ok();
        }
    }
}
