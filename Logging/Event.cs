using System;
using NLog;

namespace ProcessGremlin.Logging
{
    public abstract class Event : IEvent
    {
        protected Event(Type eventSource)
        {
            Time = DateTime.UtcNow;
            EventSource = eventSource.Name;
        }

        public string Name { get; protected set; }
        public DateTime Time { get; private set; }
        public string Detail { get; protected set; }
        public LogLevel Level { get; protected set; }

        public string EventSource { get; private set; }
    }
}