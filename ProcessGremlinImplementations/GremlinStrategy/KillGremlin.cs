using System;
using Logging;
using Logging.Events;
using ProcessGremlin.ProcessGremlin;

namespace ProcessGremlinImplementations.GremlinStrategy
{
    public class KillGremlin
    {
        private readonly SimpleProcessGremlin _gremlin;
        private readonly IEventLogger _logger;
        private static readonly Type Type = typeof (KillGremlin);

        public KillGremlin(IProcessFinder processFinder, IEventLogger logger)
        {
            _logger = logger;
            _gremlin = new SimpleProcessGremlin(
                processes =>
                {
                    _logger.Log(new IntervalStartingEvent("Process Kill Task", KillGremlin.Type));
                    try
                    {
                        foreach (var process in processes)
                        {
                            process.Kill();
                            _logger.Log(new ProcessKilledEvent(process, KillGremlin.Type));
                        }
                    }
                    catch (Exception exception)
                    {
                        _logger.Log(new ErrorEvent(exception, Type));
                    }
                    _logger.Log(new IntervalEndingEvent("Process Kill Task", KillGremlin.Type));
                }, processFinder);
        }

        public void Meddle()
        {
            _gremlin.Meddle();
        }
    }
}