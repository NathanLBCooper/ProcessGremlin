using System.Linq;
using ProcessGremlin.Core.Processes;

namespace ProcessGremlin.Implementations.Finders
{
    /// <summary>
    /// Find the first "limit" processes of the results of another "finder"
    /// For when you want to only kill a few of the found processes
    /// </summary>
    public class LimitedNumberFinder : FilteredFinder, IProcessFinder
    {
        public LimitedNumberFinder(IProcessFinder finder, int limit)
            : base(finder, processes => processes.Take(limit))
        {
        }
    }
}