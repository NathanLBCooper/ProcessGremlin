using System;
using ProcessGremlinImplementations.Logging;
using ProcessGremlinImplementations.Logging.Events;
using ProcessGremlins;

namespace ProcessGremlinImplementations.GremlinStrategy
{
    public class KillGremlin : IGremlin
    {
        private readonly SimpleProcessGremlin gremlin;
        private readonly IEventLogger logger;
        private static readonly Type Type = typeof (KillGremlin);

        public KillGremlin(IProcessFinder processFinder, IEventLogger logger)
        {
            this.logger = logger;
            this.gremlin = new SimpleProcessGremlin(
                processes =>
                {
                    this.logger.Log(new IntervalStartingEvent("Process Kill Task", KillGremlin.Type));
                    try
                    {
                        foreach (var process in processes)
                        {
                            process.Kill();
                            this.logger.Log(new ProcessKilledEvent(process, KillGremlin.Type));
                        }
                    }
                    catch (Exception exception)
                    {
                        this.logger.Log(new ErrorEvent(exception, Type));
                    }
                    this.logger.Log(new IntervalEndingEvent("Process Kill Task", KillGremlin.Type));
                }, processFinder);
        }

        public void Meddle()
        {
            this.gremlin.Meddle();
        }
    }
}