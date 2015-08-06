using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ProcessGremlins
{
    public class GremlinTimerBuilder
    {
        public GenericTimer CreateGremlinTimer<T>(Func<T> finder, IGremlin<T> gremlinstrategy, double intervalMs)
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
