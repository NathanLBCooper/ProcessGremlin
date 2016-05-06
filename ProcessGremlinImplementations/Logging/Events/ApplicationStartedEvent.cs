using System;
using NLog;

namespace ProcessGremlinImplementations.Logging.Events
{
    public sealed class ApplicationStartedEvent : Event
    {
        public ApplicationStartedEvent(Type source)
            : base(source)
        {
            Detail = "Application Starting";
            Level = LogLevel.Info;
            Name = "Application Starting";
        }
    }
}