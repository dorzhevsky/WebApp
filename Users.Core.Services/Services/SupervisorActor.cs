﻿using Akka.Actor;
using Akka.DependencyInjection;
using Akka.Event;
using Microsoft.Extensions.DependencyInjection;
using Users.Core.Services.Interfaces;

namespace Users.Core.Services.Services
{
    internal class SupervisorActor : ReceiveActor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope _scope;

        protected ILoggingAdapter Log { get; } = Context.GetLogger();

        public SupervisorActor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _scope = _serviceProvider.CreateScope();

            Receive<ProcessUsers>(HandleProcessUsers);
        }

        private void HandleProcessUsers(ProcessUsers message)
        {
            var props = DependencyResolver.For(Context.System).Props<UsersProcessorActor>();
            var usersProcessorActor = Context.ActorOf(props, $"users-processor");
            usersProcessorActor.Tell(message);
        }

        protected override SupervisorStrategy SupervisorStrategy() 
            => Akka.Actor.SupervisorStrategy.StoppingStrategy;

        protected override void PreStart()
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
            _scope.Dispose();
        }
    }
}
