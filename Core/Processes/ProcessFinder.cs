using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProcessGremlin.Core.Processes
{
    public class ProcessFinder : IProcessFinder
    {
        private readonly Func<IEnumerable<Process>> _finderFunc;

        public ProcessFinder(Func<IEnumerable<Process>> finderFunc)
        {
            _finderFunc = finderFunc;
        }

        public ProcessFinder(IEnumerable<IProcessFinder> finders)
        {
            _finderFunc = () => finders.SelectMany(finder => finder.Find());
        }

        public IEnumerable<Process> Find()
        {
            return _finderFunc.Invoke();
        }
    }
}