using External.Contracts;
using MediatR;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;

namespace External.Api.External
{
    public class ExternalController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IBus _bus;

        public ExternalController(IMediator mediator, IBus bus)
        {
            _mediator = mediator;
            _bus = bus;
        }

        [Route("api/external/execute")]
        [HttpGet]
        public async Task<IActionResult> Execute()
        {
            await _mediator.Publish(new ExternalNotification());
            return Json(null);
        }

        [Route("api/external/process")]
        [HttpGet]
        public async Task<IActionResult> Process()
        {
            await _bus.Publish(new ProcessUsers());
            return Json(null);
        }
    }
}
