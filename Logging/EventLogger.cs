using System.Collections.Concurrent;
using NLog;

namespace Logging
{
    public class EventLogger : IEventLogger
    {
        private static readonly ConcurrentDictionary<string, Logger> Loggers = new ConcurrentDictionary<string, Logger>();

        public void Log(IEvent evt)
        {
            var logger = GetLogger(evt);
            logger.Log(evt.Level, string.Format("{0} : {1} : {2}", evt.Time, evt.Name, evt.Detail));
        }

        private static Logger GetLogger(IEvent evt)
        {
            return Loggers.GetOrAdd(evt.EventSource, LogManager.GetLogger);
        }
    }
}