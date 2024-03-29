using LoggerManager;
using Microsoft.AspNetCore.Mvc;
using Users.Core.Services.Interfaces;

namespace Users.Adapter.Api.Users
{
    public class UsersController : Controller
    {
        private readonly ILoggingManager _logger;
        private readonly IUsersService _service;

        public UsersController(IUsersService service, ILoggingManager logger)
        {
            _logger = logger;
            _service = service;
        }

        [Route("api/users/getExternalData")]
        [HttpGet]
        public async Task<IActionResult> GetDataFromExternalModule()
        {
            string data = await _service.GetDataFromExternalModule();
            return Json(data);
        }

        [Route("api/users/getUsers")]
        [HttpGet]
        public async Task<IActionResult> GetDataFromDatabase()
        {
            _logger.LogDebug("Debug1");
            var data = await _service.GetUsers();
            return Json(data);
        }

        [Route("api/module2/data3")]
        [HttpGet]
        public async Task<IActionResult> SendNotification()
        {
            await _service.SendNotification();
            return Json(null);
        }
    }
}
