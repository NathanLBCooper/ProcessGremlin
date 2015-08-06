using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProcessGremlin
{
    public class GremlinBuilder
    {
        public Action<IEnumerable<Process>> KillAll()
        {
            return processes =>
            {
                foreach (var process in processes)
                {
                    process.Kill();
                }
            };
        }

        public Action<IEnumerable<Process>> KillRandom(Random random)
        {
            return processes =>
            {
                var processList = processes.ToList();
                if (processList.Count == 0) return;
                processList[random.Next(0, processList.Count)].Kill();        
            };
        }
    }
}