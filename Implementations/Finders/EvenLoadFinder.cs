using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProcessGremlin.Core.Processes;
using ProcessGremlin.Logging;

namespace ProcessGremlin.Implementations.Finders
{
    public class EvenLoadFinder : IProcessFinder
    {
        private readonly IEnumerator<IProcessFinder> _processFinderEnumerator;
        private readonly IEnumerable<IProcessFinder> _processFinders;

        public EvenLoadFinder(IEnumerable<IProcessFinder> processFinders, IEventLogger logger)
        {
            _processFinders = processFinders;
            _processFinderEnumerator = _processFinders.GetEnumerator();
        }

        public IEnumerable<Process> Find()
        {
            for (var i = 0; i < _processFinders.Count(); i++)
            {
                if (!_processFinderEnumerator.MoveNext())
                {
                    _processFinderEnumerator.Reset();
                    _processFinderEnumerator.MoveNext();
                }

                var processesOfName = _processFinderEnumerator.Current.Find().ToList();
                if (processesOfName.Count != 0)
                {
                    return processesOfName.Take(1);
                }
            }

            return new List<Process>(0);
        }
    }
}