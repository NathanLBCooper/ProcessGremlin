using System;
using NLog;

namespace ProcessGremlinImplementations.Logging.Events
{
    public sealed class ErrorEvent : Event
    {
        public ErrorEvent(string message, Type source)
            : base(source)
        {
            Detail = message;
            Level = LogLevel.Error;
            Name = "Error";
        }

        public ErrorEvent(Exception exception, Type source)
            : this(exception.ToString(), source)
        {
        }
    }
}