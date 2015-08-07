using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ProcessGremlins;

namespace ProcessGremlinImplementations
{
    public class ProcessFinderBuilder
    {
        public IProcessFinder GetNameBasedFinder(string name)
        {
            return new ProcessFinder(() => Process.GetProcessesByName(name));
        }

        public IEnumerable<IProcessFinder> GetMultipleNameFinders(IEnumerable<string> names)
        {
            return names.Select(this.GetNameBasedFinder);
        }

        public IProcessFinder GetNameBasedFinder(IEnumerable<string> names)
        {
            return new ProcessFinder(this.GetMultipleNameFinders(names));
        }
    }
}