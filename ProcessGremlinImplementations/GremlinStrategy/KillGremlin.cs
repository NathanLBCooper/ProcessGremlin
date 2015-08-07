using System;

using ProcessGremlins;

namespace ProcessGremlinImplementations
{
    public class KillGremlin : IGremlin
    {
        private readonly SimpleProcessGremlin gremlin;

        public KillGremlin(IProcessFinder processFinder)
        {
            this.gremlin = new SimpleProcessGremlin(
                processes =>
                {
                    foreach (var process in processes)
                    {
                        Console.WriteLine("killing {0}", process.ProcessName); //todo nlog
                        process.Kill();
                    }
                }, processFinder);
        }

        public void Meddle()
        {
            this.gremlin.Meddle();
        }
    }
}