using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProcessGremlin.Core.Processes;

namespace ProcessGremlin.Implementations.Finders
{
    public class LimitedNumberFinder : IProcessFinder
    {
        private readonly IProcessFinder _finder;
        private readonly int _limit;

        // Efficiency of implementation depends on IEnumerable<Process> returned by _finder being lazy
        public LimitedNumberFinder(IProcessFinder finder, int limit)
        {
            _finder = finder;
            _limit = limit;
        }

        public IEnumerable<Process> Find()
        {
            return this._finder.Find().Take(_limit);
        }
    }
}