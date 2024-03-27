using Akka.Actor;
using Akka.Event;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Users.Core.Services.Interfaces;
using Users.Core.Services.Messages;

namespace Users.Core.Services.Services
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
            var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
            mediator.Publish(new UpdateUsers());
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
