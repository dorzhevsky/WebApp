using Akka.Actor;
using Akka.Event;
using Microsoft.Extensions.DependencyInjection;
using Users.Services.Interfaces;

namespace Users.Services.Services
{
    internal class UsersProcessorActor : ReceiveActor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope _scope;

        protected ILoggingAdapter Log { get; } = Context.GetLogger();

        public UsersProcessorActor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _scope = _serviceProvider.CreateScope();

            Receive<ProcessUsers>(HandleProcessUsers);
        }

        private void HandleProcessUsers(ProcessUsers message)
        {
            var usersService = _scope.ServiceProvider.GetRequiredService<IUsersService>();
            usersService.Update();
        }

        protected override void PreStart()
        {
            base.PreStart();
        }

        protected override void PostStop()
        {
            base.PostStop();
            _scope.Dispose();
        }
    }
}
