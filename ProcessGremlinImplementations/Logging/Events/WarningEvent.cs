using System;
using NLog;

namespace ProcessGremlinImplementations.Logging.Events
{
    public sealed class WarningEvent : Event
    {
        public WarningEvent(string message, Type source)
            : base(source)
        {
            this.Detail = message;
            this.Level = LogLevel.Warn;
            this.Name = "Warning";
        }

        public WarningEvent(Exception exception, Type source)
            : this(exception.ToString(), source)
        {
        }
    }
}