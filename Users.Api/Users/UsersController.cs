using Microsoft.AspNetCore.Mvc;
using Users.Application.Interfaces;

namespace Users.Api.Users
{
    public class UsersController : Controller
    {
        private readonly IUsersService _service;

        public UsersController(IUsersService service)
        {
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
