using System;
using System.Collections.Generic;
using System.Linq;

using ProcessGremlins;

namespace ProcessGremlinImplementations
{
    public class EvenLoadPerNameGremlin : IGremlin
    {
        private readonly IEnumerable<IProcessFinder> processFinders;
        private readonly IEnumerator<IProcessFinder> processFinderEnumerator;

        public EvenLoadPerNameGremlin(IEnumerable<IProcessFinder> processFinders)
        {
            this.processFinders = processFinders;
            this.processFinderEnumerator = this.processFinders.GetEnumerator();
        }

        public EvenLoadPerNameGremlin(List<string> processNames, ProcessFinderBuilder finderBuilder)
            : this(finderBuilder.GetMultipleNameFinders(processNames))
        {
        }

        public void Meddle()
        {
            for (int i = 0; i < this.processFinders.Count(); i++)
            {
                if (!this.processFinderEnumerator.MoveNext())
                {
                    processFinderEnumerator.Reset();
                    processFinderEnumerator.MoveNext();
                }

                var processesOfName = this.processFinderEnumerator.Current.Find().ToList();
                if (processesOfName.Count != 0)
                {
                    Console.WriteLine("Killed {0}", processesOfName.First().ProcessName);
                    processesOfName.First().Kill();
                    return;
                }
            }
        }
    }
}