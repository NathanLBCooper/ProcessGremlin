using System;
using NLog;

namespace ProcessGremlinImplementations.Logging.Events
{
    public sealed class ApplicationStartedEvent : Event
    {
        public ApplicationStartedEvent(Type source)
            : base(source)
        {
            this.Detail = "Application Starting";
            this.Level = LogLevel.Info;
            this.Name = "Application Starting";
        }
    }
}