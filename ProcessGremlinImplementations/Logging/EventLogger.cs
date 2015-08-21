using NLog;

namespace ProcessGremlinImplementations.Logging
{
    public class EventLogger : IEventLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void Log(IEvent evt)
        {
            EventLogger.Logger.Log(evt.Level, string.Format("{0} : {1} : {2}", evt.Time, evt.Name, evt.Detail));
        }
    }
}