using System;
using System.Collections.Generic;
using System.Diagnostics;
using ProcessGremlin.Core.Processes;

namespace ProcessGremlin.Implementations.Finders
{
    /// <summary>
    /// Passes the results of a IProcessFinder through a filter
    /// For when you want to only kill a subset of the found processes
    /// The filter should do something like, for example, pick the first result
    /// </summary>
    public class FilteredFinder
    {
        private readonly IProcessFinder _finder;
        private readonly Func<IEnumerable<Process>, IEnumerable<Process>> _filter; 

        // Efficiency of implementation depends on IEnumerable<Process> returned by _finder being lazy
        public FilteredFinder(IProcessFinder finder, Func<IEnumerable<Process>, IEnumerable<Process>> filter)
        {
            _finder = finder;
            _filter = filter;
        }

        public IEnumerable<Process> Find()
        {
            return _filter(_finder.Find());
        }
    }
}
