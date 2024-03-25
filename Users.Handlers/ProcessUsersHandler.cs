using Rebus.Handlers;
using Users.Services.Interfaces;

namespace Users.Handlers
{
    internal sealed class ProcessUsersHandler : IHandleMessages<Messages.ProcessUsers>
    {
        private readonly IUsersHostedService _usersHostedService;

        public ProcessUsersHandler(IUsersHostedService usersHostedService)
        {
            _usersHostedService = usersHostedService;
        }

        public Task Handle(Messages.ProcessUsers @event)
        {
            _usersHostedService.Tell(new ProcessUsers());
            return Task.CompletedTask;
        }
    }
}
