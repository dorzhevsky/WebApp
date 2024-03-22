using External.Contracts;
using MediatR;
using Users.HostedServices.Interfaces;

namespace Users.Handlers
{
    public class ExternalNotificationdHandler : INotificationHandler<ExternalNotification>
    {
        private readonly IUsersHostedService _service;
        public ExternalNotificationdHandler(IUsersHostedService service)
        {
            _service = service;
        }

        public async Task Handle(ExternalNotification notification, CancellationToken cancellationToken)
        {
            _service.Tell(new ProcessUsers());
        }
    }
}