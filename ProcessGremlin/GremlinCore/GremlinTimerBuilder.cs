using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ProcessGremlins
{
    public class GremlinTimerBuilder
    {
        public GenericTimer CreateGremlinTimer(Func<IEnumerable<Process>> finder, IProcessGremlin gremlinstrategy, double intervalMs)
        {
            return new GenericTimer(
                () =>
                {
                    gremlinstrategy.Invoke(finder.Invoke());
                },
                intervalMs);
        }
    }
}
