using System;
using NLog;

namespace ProcessGremlinImplementations.Logging.Events
{
    public sealed class IntervalEndingEvent : Event
    {
        public IntervalEndingEvent(string description, Type source)
            : base(source)
        {
            Detail = string.Format("Action ending: {0}", description);
            Level = LogLevel.Debug;
            Name = "Interval Ending";
        }
    }
}