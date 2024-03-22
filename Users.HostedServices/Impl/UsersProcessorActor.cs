using Akka.Actor;
using Akka.Event;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Interfaces;

namespace Users.HostedServices.Impl
{
    internal class UsersProcessorActor : ReceiveActor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope _scope;
        private readonly IUsersService _usersService;

        protected ILoggingAdapter Log { get; } = Context.GetLogger();

        public UsersProcessorActor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _scope = _serviceProvider.CreateScope();

            _usersService = _scope.ServiceProvider.GetRequiredService<IUsersService>();
        }

        protected async override void PreStart()
        {
            base.PreStart();
            await _usersService.GetUsers();
        }

        protected override void PostStop()
        {
            base.PostStop();
            _scope.Dispose();
        }
    }
}
