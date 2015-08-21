using NLog;

namespace ProcessGremlinImplementations.Logging.Events
{
    public sealed class IntervalStartingEvent : Event
    {
        public IntervalStartingEvent(string description)
        {
            this.Detail = string.Format("Action begining: {0}", description);
            this.Level = LogLevel.Debug;
            this.Name = "Interval Starting";
        }
    }
}