using External.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace External.Api.External
{
    public class ExternalController : Controller
    {
        private readonly IMediator _mediator;

        public ExternalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("api/external/execute")]
        [HttpGet]
        public async Task<IActionResult> GetDataFromExternalModule()
        {
            await _mediator.Publish(new ExternalNotification());
            return Json(null);
        }
    }
}
