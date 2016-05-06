using System;

namespace ProcessGremlin.GremlinCore
{
    public class GremlinTimerBuilder
    {
        public GenericTimer CreateGremlinTimer(Action gremlin, double intervalMs)
        {
            return new GenericTimer(gremlin.Invoke, intervalMs);
        }
    }
}