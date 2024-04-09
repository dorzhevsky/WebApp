using Domain.Contracts.Modules.External;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Modules.External.Adapter.Api.External
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
        public async Task<IActionResult> Execute()
        {
            await _mediator.Publish(new ExternalNotification());
            return Json(null);
        }
    }
}
