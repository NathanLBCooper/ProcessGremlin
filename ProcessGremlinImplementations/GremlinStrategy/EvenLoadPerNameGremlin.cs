using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ProcessGremlins;

namespace ProcessGremlinImplementations
{
    // Just an example, I don't particularly stand by this implementation
    public class EvenLoadPerNameGremlin : IProcessGremlin
    {
        private readonly Queue<string> queue = new Queue<string>(); 

        public void Invoke(IEnumerable<Process> data)
        {
            var processList = data.ToList();
            var groupedByProcess = processList.GroupBy(x => x.ProcessName).Select(y => y.ToList()).OrderBy(z => z.First().ProcessName).ToList();

            if (groupedByProcess.Count == 0)
            {
                return;
            }

            List<string> names = groupedByProcess.Select(
                x =>
                {
                    var firstProcess = x.FirstOrDefault();
                    if (firstProcess == null) return null;
                    return firstProcess.ProcessName;
                }).ToList();

            
            var undiscovered = names.Where(x => !queue.Contains(x)).ToList();

            if (undiscovered.Count > 0)
            {
                this.StopFirstOfName(undiscovered.First(), processList);
                return;
            }

            string leastRecentlyStopped = null;
            while (this.queue.Count > 0)
            {
                var lastOfQueue = this.queue.Dequeue();
                if (names.Contains(lastOfQueue))
                {
                    leastRecentlyStopped = lastOfQueue;
                    break;
                }
            }

            if (leastRecentlyStopped != null)
            {
                this.StopFirstOfName(leastRecentlyStopped, processList);
                return;
            }

            this.StopFirstOfName(names.First(), processList);
        }

        private void StopFirstOfName(string name, IEnumerable<Process> processes)
        {
            var process = processes.First(x => x.ProcessName == name);
            process.Kill();
            this.queue.Enqueue(name);
        }
    }
}