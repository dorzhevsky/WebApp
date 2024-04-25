using Akka.Actor;
using NLog;
using System.Diagnostics;

namespace Modules.Users.Core.Services.Services
{
    internal abstract class TracedReceiveActor: ReceiveActor
    {
        protected readonly ILogger _logger;
        protected TracedReceiveActor()
        {
            _logger = LogManager.GetLogger(GetType().ToString());
        }

        protected override bool AroundReceive(Receive receive, object message)
        {
            using var activity = new Activity(ActivityName(message));
            var tracedMessage = message as TracedMessage;
            if (tracedMessage?.ActivityId is not null) {
                activity.SetParentId(tracedMessage.ActivityId);
            }
            activity.Start();
            if (tracedMessage is not null) {
                _logger.Log(GetType(), new LogEventInfo(LogLevel.Info, _logger.Name, tracedMessage.ToString()));
            }            
            return base.AroundReceive(receive, message);
        }

        public override void AroundPreRestart(Exception cause, object message)
        {
            base.AroundPreRestart(cause, message);
        }

        protected virtual string ActivityName(object message)  => $"Akka.Messaging";
    }
}
