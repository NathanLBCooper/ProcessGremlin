using System;
using NLog;

namespace ProcessGremlinImplementations.Logging.Events
{
    public sealed class FailureEvent : Event
    {
        public FailureEvent(string message)
        {
            this.Detail = message;
            this.Level = LogLevel.Fatal;
            this.Name = "Failure";
        }

        public FailureEvent(Exception exception)
        {
            this.Detail = exception.ToString();
            this.Level = LogLevel.Fatal;
            this.Name = "Failure";
        }
    }
}