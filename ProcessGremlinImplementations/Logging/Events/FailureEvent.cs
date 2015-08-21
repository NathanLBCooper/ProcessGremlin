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
            : this(exception.ToString())
        {
        }

        public FailureEvent(string message, Exception exception)
            : this(string.Format("Message: {0} Exception: {1}", message, exception.ToString()))
        {
        }
    }
}