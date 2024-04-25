using System.Diagnostics;

namespace Modules.Users.Core.Services
{
    public class TracedMessage
    {
        public TracedMessage()
        {
            TraceId = Activity.Current?.TraceId;
            ActivityId = Activity.Current?.Id;
        }

        public ActivityTraceId? TraceId { get; init; }
        public string? ActivityId { get; init;  }
    }
}
