using Domain.Contracts.Modules.External;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Modules.External.Adapter.Api.External
{
    public class ExternalController : Controller
    {
        private readonly IMediator _mediator;
        private static ILogger<ExternalController> _logger;

        public ExternalController(IMediator mediator, ILogger<ExternalController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Route("api/external/execute")]
        [HttpGet]
        public async Task<IActionResult> Execute()
        {
            throw new Exception("ewtwqt");
            _logger.LogInformation("ExternalController.Execute");
            await _mediator.Publish(new ExternalNotification());
            return Json(null);
        }
    }
}
