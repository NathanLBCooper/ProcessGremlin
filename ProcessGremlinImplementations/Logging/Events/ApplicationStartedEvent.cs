using NLog;

namespace ProcessGremlinImplementations.Logging
{
    public sealed class ApplicationStartedEvent : Event
    {
        public ApplicationStartedEvent()
        {
            this.Detail = "Application Starting";
            this.Level = LogLevel.Info;
            this.Name = "Application Starting";
        }
    }
}