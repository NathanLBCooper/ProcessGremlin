using System;
using NLog;

namespace ProcessGremlinImplementations.Logging.Events
{
    public sealed class FailureEvent : Event
    {
        public FailureEvent(string message, Type source)
            : base(source)
        {
            this.Detail = message;
            this.Level = LogLevel.Fatal;
            this.Name = "Failure";
        }

        public FailureEvent(Exception exception, Type source)
            : this(exception.ToString(), source)
        {
        }

        public FailureEvent(string message, Exception exception, Type source)
            : this(string.Format("Message: {0} Exception: {1}", message, exception.ToString()), source)
        {
        }
    }
}