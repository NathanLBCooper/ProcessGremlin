using NLog;

namespace ProcessGremlinImplementations.Logging.Events
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