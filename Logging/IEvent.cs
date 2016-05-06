using System;
using NLog;

namespace ProcessGremlin.Logging
{
    public interface IEvent
    {
        string Name { get; }
        DateTime Time { get; }
        string Detail { get; }
        LogLevel Level { get; }
        string EventSource { get; }
    }
}