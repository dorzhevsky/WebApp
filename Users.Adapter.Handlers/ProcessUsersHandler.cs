using Rebus.Handlers;
using Users.Core.Services.Interfaces;

namespace Users.Adapter.Handlers
{
    internal sealed class ProcessUsersHandler : IHandleMessages<ProcessUsers>
    {
        private readonly IUsersHostedService _usersHostedService;

        public ProcessUsersHandler(IUsersHostedService usersHostedService)
        {
            _usersHostedService = usersHostedService;
        }

        public Task Handle(ProcessUsers @event)
        {
            _usersHostedService.Tell(new ProcessUsers());
            return Task.CompletedTask;
        }
    }
}
