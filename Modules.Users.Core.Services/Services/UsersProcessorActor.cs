using Akka.Actor;
using Akka.Event;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Modules.Users.Core.Services.Messages;
using NLog;
using System.Threading.Tasks.Dataflow;

namespace Modules.Users.Core.Services.Services
{
    internal class UsersProcessorActor : TracedReceiveActor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope _scope;
        private ActionBlock<int> _actionBlock;
        private NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        protected ILoggingAdapter Log { get; } = Context.GetLogger();

        public UsersProcessorActor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _scope = _serviceProvider.CreateScope();

            Receive<ProcessUsersMessage>(HandleProcessUsers);
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

        private void HandleProcessUsers(ProcessUsersMessage message)
        {
            logger.Info("Something");
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
