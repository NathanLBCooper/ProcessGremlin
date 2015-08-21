using System;
using NLog;

namespace ProcessGremlinImplementations.Logging
{
    public interface IEvent
    {
        string Name { get; }
        DateTime Time { get; }
        string Detail { get; }
        LogLevel Level { get; }
    }
}