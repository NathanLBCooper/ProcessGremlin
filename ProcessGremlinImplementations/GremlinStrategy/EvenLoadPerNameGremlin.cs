using System;
using System.Collections.Generic;
using System.Linq;

using ProcessGremlins;
using ProcessGremlinImplementations.Logging;

namespace ProcessGremlinImplementations
{
    public class EvenLoadPerNameGremlin : IGremlin
    {
        private readonly IEnumerable<IProcessFinder> processFinders;
        private readonly IEnumerator<IProcessFinder> processFinderEnumerator;
        private readonly IEventLogger logger;

        public EvenLoadPerNameGremlin(IEnumerable<IProcessFinder> processFinders, IEventLogger logger)
        {
            this.processFinders = processFinders;
            this.processFinderEnumerator = this.processFinders.GetEnumerator();
            this.logger = logger;
        }

        public EvenLoadPerNameGremlin(List<string> processNames, ProcessFinderBuilder finderBuilder, IEventLogger logger)
            : this(finderBuilder.GetMultipleNameFinders(processNames), logger)
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
                    var process = processesOfName.First();
                    process.Kill();
                    this.logger.Log(new ProcessKilledEvent(process));             
                    return;
                }
            }
        }
    }
}