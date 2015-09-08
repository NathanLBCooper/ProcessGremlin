using System;
using NLog;

namespace ProcessGremlinImplementations.Logging.Events
{
    public sealed class IntervalEndingEvent : Event
    {
        public IntervalEndingEvent(string description, Type source)
            : base(source)
        {
            this.Detail = string.Format("Action ending: {0}", description);
            this.Level = LogLevel.Debug;
            this.Name = "Interval Ending";
        }
    }
}