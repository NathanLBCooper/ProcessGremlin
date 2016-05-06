using System;

namespace ProcessGremlins
{
    public class GremlinTimerBuilder
    {
        private readonly ITimerFactory timerFactory = new TimerFactory();

        public GenericTimer CreateGremlinTimer(IGremlin gremlin, double intervalMs)
        {
            return new GenericTimer(gremlin.Meddle, intervalMs, timerFactory);
        }

        public GenericTimer CreateGremlinTimer(Action gremlin, double intervalMs)
        {
            return new GenericTimer(gremlin.Invoke, intervalMs, timerFactory);
        }
    }
}