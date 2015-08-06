using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProcessGremlin
{
    public class FinderBuilder
    {
        public Func<IEnumerable<Process>> GetNameBasedFinder(string name)
        {
            return () => Process.GetProcessesByName(name);
        }

        public Func<IEnumerable<Process>> ConcatFinders(IEnumerable<Func<IEnumerable<Process>>> finders)
        {
            return () =>
            { return finders.SelectMany(finder => finder.Invoke()); };
        }
    }
}