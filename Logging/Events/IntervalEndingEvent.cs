using System;
using NLog;

namespace ProcessGremlin.Logging.Events
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