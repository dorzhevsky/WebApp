using Akka.Actor;
using Akka.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Users.Core.Services.Interfaces;

namespace Users.Core.Services.Services
{
    internal sealed class UsersHostedService : IHostedService, IUsersHostedService
    {
        private IActorRef _actorRef;
        private ActorSystem _actorSystem;
        private readonly IServiceProvider _serviceProvider;

        private readonly IHostApplicationLifetime _applicationLifetime;

        public UsersHostedService(IServiceProvider sp, IHostApplicationLifetime applicationLifetime)
        {
            _serviceProvider = sp;
            _applicationLifetime = applicationLifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //var hocon = ConfigurationFactory.ParseString(
            //        await File.ReadAllTextAsync("app.conf", cancellationToken));

            var bootstrap = BootstrapSetup.Create();//.WithConfig(hocon);

            // enable DI support inside this ActorSystem, if needed
            var diSetup = DependencyResolverSetup.Create(_serviceProvider);

            // merge this setup (and any others) together into ActorSystemSetup
            var actorSystemSetup = bootstrap.And(diSetup);

            _actorSystem = ActorSystem.Create("users-processing", actorSystemSetup);
            var props = DependencyResolver.For(_actorSystem).Props<SupervisorActor>();
            _actorRef = _actorSystem.ActorOf(props, "users-supervisor");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // strictly speaking this may not be necessary - terminating the ActorSystem would also work
            // but this call guarantees that the shutdown of the cluster is graceful regardless
            await CoordinatedShutdown.Get(_actorSystem).Run(CoordinatedShutdown.ClrExitReason.Instance);
        }

        public void Tell(object message)
        {
            _actorRef?.Tell(message);
        }
    }
}