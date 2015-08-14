using System;

using NLog;

namespace ProcessGremlinImplementations.Logging
{
    public sealed class ErrorEvent : Event
    {
        public ErrorEvent(string message)
        {
            this.Detail = message;
            this.Level = LogLevel.Error;
            this.Name = "Error";
        }

        public ErrorEvent(Exception exception)
        {
            this.Detail = exception.ToString();
            this.Level = LogLevel.Error;
            this.Name = "Error";
        }
    }
}