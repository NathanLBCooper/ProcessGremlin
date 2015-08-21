using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProcessGremlins;

namespace ProcessGremlinImplementations.Finders
{
    public class LimitedNumberFinder : IProcessFinder
    {
        private readonly IProcessFinder finder;
        private readonly int limit;

        // Efficiency of implementation depends on IEnumerable<Process> returned by finder being lazy
        public LimitedNumberFinder(IProcessFinder finder, int limit)
        {
            this.finder = finder;
            this.limit = limit;
        }

        public IEnumerable<Process> Find()
        {
            return this.finder.Find().Take(this.limit);
        }
    }
}