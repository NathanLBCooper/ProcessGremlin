using System;
using NLog;

namespace ProcessGremlinImplementations.Logging
{
    public abstract class Event : IEvent
    {
        protected Event()
        {
            this.Time = DateTime.UtcNow;
        }

        public string Name { get; protected set; }
        public DateTime Time { get; private set; }
        public string Detail { get; protected set; }
        public LogLevel Level { get; protected set; }
    }
}