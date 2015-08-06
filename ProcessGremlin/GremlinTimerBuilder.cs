using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ProcessGremlin
{
    public class GremlinTimerBuilder
    {
        public GenericTimer CreateGremlinTimer(Func<IEnumerable<Process>> finder, Action<IEnumerable<Process>> gremlinstrategy, double intervalMs)
        {
            return new GenericTimer(
                () =>
                {
                    gremlinstrategy(finder.Invoke());
                },
                intervalMs);
        }
    }
}
