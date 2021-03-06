using System;
using NLog;

namespace ProcessGremlin.Logging.Events
{
    public sealed class ApplicationEndingEvent : Event
    {
        public ApplicationEndingEvent(Type source)
            : base(source)
        {
            Detail = "Application Ending";
            Level = LogLevel.Info;
            Name = "Application Ending";
        }
    }
}