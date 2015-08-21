using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProcessGremlinImplementations.Logging;
using ProcessGremlins;

namespace ProcessGremlinImplementations.Finders
{
    public class EvenLoadFinder : IProcessFinder
    {
        private readonly IEnumerator<IProcessFinder> processFinderEnumerator;
        private readonly IEnumerable<IProcessFinder> processFinders;

        public EvenLoadFinder(IEnumerable<IProcessFinder> processFinders, IEventLogger logger)
        {
            this.processFinders = processFinders;
            this.processFinderEnumerator = this.processFinders.GetEnumerator();
        }

        public IEnumerable<Process> Find()
        {
            for (var i = 0; i < this.processFinders.Count(); i++)
            {
                if (!this.processFinderEnumerator.MoveNext())
                {
                    this.processFinderEnumerator.Reset();
                    this.processFinderEnumerator.MoveNext();
                }

                var processesOfName = this.processFinderEnumerator.Current.Find().ToList();
                if (processesOfName.Count != 0)
                {
                    return processesOfName.Take(1);
                }
            }

            return new List<Process>(0);
        }
    }
}