using System;
using NLog;

namespace ProcessGremlinImplementations.Logging.Events
{
    public sealed class IntervalStartingEvent : Event
    {
        public IntervalStartingEvent(string description, Type source)
            : base(source)
        {
            Detail = string.Format("Action begining: {0}", description);
            Level = LogLevel.Debug;
            Name = "Interval Starting";
        }
    }
}