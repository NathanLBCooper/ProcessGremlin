using System;

using ProcessGremlinImplementations.Logging;

using ProcessGremlins;

namespace ProcessGremlinImplementations
{
    public class KillGremlin : IGremlin
    {
        private readonly SimpleProcessGremlin gremlin;
        private readonly IEventLogger logger;

        public KillGremlin(IProcessFinder processFinder, IEventLogger logger)
        {
            this.gremlin = new SimpleProcessGremlin(
                processes =>
                {
                    try
                    {
                        foreach (var process in processes)
                        {
                            process.Kill();
                            this.logger.Log(new ProcessKilledEvent(process));
                        }
                    }
                    catch (Exception exception)
                    {
                        this.logger.Log(new ErrorEvent(exception));
                    }
                }, processFinder);
        }

        public void Meddle()
        {
            this.gremlin.Meddle();
        }
    }
}