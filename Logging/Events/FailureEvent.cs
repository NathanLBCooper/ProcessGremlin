using System;
using NLog;

namespace ProcessGremlin.Logging.Events
{
    public sealed class FailureEvent : Event
    {
        public FailureEvent(string message, Type source)
            : base(source)
        {
            Detail = message;
            Level = LogLevel.Fatal;
            Name = "Failure";
        }

        public FailureEvent(Exception exception, Type source)
            : this(exception.ToString(), source)
        {
        }

        public FailureEvent(string message, Exception exception, Type source)
            : this(string.Format("Message: {0} Exception: {1}", message, exception), source)
        {
        }
    }
}