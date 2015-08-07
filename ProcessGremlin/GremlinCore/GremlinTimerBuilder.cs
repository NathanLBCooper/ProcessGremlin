using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ProcessGremlins
{
    public class GremlinTimerBuilder
    {
        public GenericTimer CreateGremlinTimer(IGremlin gremlin, double intervalMs)
        {
            return new GenericTimer(gremlin.Invoke, intervalMs);
        }

        public GenericTimer CreateGremlinTimer(Action gremlin, double intervalMs)
        {
            return new GenericTimer(gremlin.Invoke, intervalMs);
        }
    }
}
