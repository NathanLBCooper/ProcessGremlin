using System;
using ProcessGremlin.ProcessGremlin;

namespace ProcessGremlin.GremlinCore
{
    public class GremlinTimerBuilder
    {
        public GenericTimer CreateGremlinTimer(IGremlin gremlin, double intervalMs)
        {
            return new GenericTimer(gremlin.Meddle, intervalMs);
        }

        public GenericTimer CreateGremlinTimer(Action gremlin, double intervalMs)
        {
            return new GenericTimer(gremlin.Invoke, intervalMs);
        }
    }
}