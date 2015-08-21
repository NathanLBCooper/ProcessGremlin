using System;
using NLog;

namespace ProcessGremlinImplementations.Logging.Events
{
    public sealed class WarningEvent : Event
    {
        public WarningEvent(string message)
        {
            this.Detail = message;
            this.Level = LogLevel.Warn;
            this.Name = "Warning";
        }

        public WarningEvent(Exception exception)
        {
            this.Detail = exception.ToString();
            this.Level = LogLevel.Warn;
            this.Name = "Warning";
        }
    }
}