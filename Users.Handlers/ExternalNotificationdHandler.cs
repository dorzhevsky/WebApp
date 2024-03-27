﻿using Contracts.Modules.External;
using MediatR;
using Users.Core.Services.Interfaces;

namespace Users.Adapter.Handlers
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