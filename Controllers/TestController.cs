using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HuskyBot.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly IUserLevelService _userLevelService;

        public TestController(ILogger<TestController> logger, IUserLevelService userLevelService)
        {
            _userLevelService = userLevelService;
            _logger = logger;
        }

        [HttpGet]
        [Route("levels")]
        public async Task<IActionResult> GetLevels()
        {
            _userLevelService.GetLevels().ForEach(level => {
                Console.WriteLine(level);
            });
            return Ok();
        }
    }
}