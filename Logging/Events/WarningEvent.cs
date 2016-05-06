using System;
using NLog;

namespace ProcessGremlin.Logging.Events
{
    public sealed class WarningEvent : Event
    {
        public WarningEvent(string message, Type source)
            : base(source)
        {
            Detail = message;
            Level = LogLevel.Warn;
            Name = "Warning";
        }

        public WarningEvent(Exception exception, Type source)
            : this(exception.ToString(), source)
        {
        }
    }
}