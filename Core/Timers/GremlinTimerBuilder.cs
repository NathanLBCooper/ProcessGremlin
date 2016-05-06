using System;

namespace ProcessGremlin.Core.Timers
{
    public class GremlinTimerBuilder
    {
        public GenericTimer CreateGremlinTimer(Action gremlin, double intervalMs)
        {
            return new GenericTimer(gremlin.Invoke, intervalMs);
        }
    }
}