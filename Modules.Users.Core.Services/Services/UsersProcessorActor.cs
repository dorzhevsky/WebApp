using Akka.Actor;
using Akka.Event;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Core.Services.Interfaces;
using System.Threading.Tasks.Dataflow;

namespace Modules.Users.Core.Services.Services
{
    internal class UsersProcessorActor : ReceiveActor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope _scope;
        private ActionBlock<int> _actionBlock;

        protected ILoggingAdapter Log { get; } = Context.GetLogger();

        public UsersProcessorActor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _scope = _serviceProvider.CreateScope();

            Receive<ProcessUsers>(HandleProcessUsers);
            Receive<Status.Failure>(HandleFailure);
            _actionBlock = new ActionBlock<int>(OnAction);
            _actionBlock.Completion.PipeTo(Self);
        }

        private void HandleFailure(Status.Failure failure)
        {
            throw new Exception();
            //Context.Stop(Self);
        }

        private void OnAction(int v)
        {
            throw new Exception();
        }

        private void HandleProcessUsers(ProcessUsers message)
        {
            //throw new Exception();
            _actionBlock.Post(0);
            //Context.Stop(Self);
            //throw new Exception();
            //var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
            //mediator.Publish(new UpdateUsers());
        }

        protected override async void PreStart()
        {
            base.PreStart();
        }

        protected override void PostRestart(Exception reason)
        {
            base.PostRestart(reason);
        }

        protected override void PostStop()
        {
            base.PostStop();
            Context.System.ActorSelection("user/*").Tell("Failed");
            _scope.Dispose();
        }
    }
}
