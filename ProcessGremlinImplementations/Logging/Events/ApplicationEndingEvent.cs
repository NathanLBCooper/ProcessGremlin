using NLog;

namespace ProcessGremlinImplementations.Logging
{
    public sealed class ApplicationEndingEvent : Event
    {
        public ApplicationEndingEvent()
        {
            this.Detail = "Application Ending";
            this.Level = LogLevel.Info;
            this.Name = "Application Ending";
        }
    }
}