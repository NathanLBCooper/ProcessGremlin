using System;
using System.Diagnostics;
using System.Linq;

using ProcessGremlins;
using ProcessGremlinImplementations.Logging;

namespace ProcessGremlinImplementations
{
    public class KillBusyGremlin : IGremlin
    {
        private readonly int busyThreshold;
        private readonly IProcessFinder finder;
        private readonly IEventLogger logger;

        // busyThreshold in % of core being used, ie up to 400 on a 4-core
        public KillBusyGremlin(IProcessFinder processFinder, int busyThreshold, IEventLogger logger)
        {
            this.busyThreshold = busyThreshold;
            this.finder = processFinder;
            this.logger = logger;
        }

        public void Meddle()
        {
            var data = this.finder.Find();
            var busyProcesses = data.Where(process => this.GetCpuUsage(process) > this.busyThreshold).ToList();
            foreach (var process in busyProcesses)
            {
                process.Kill();
                this.logger.Log(new ProcessKilledEvent(process));    
            }
        }

        private int GetCpuUsage(Process process)
        {
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", this.GetInstanceName(process), true);
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            var usage = cpuCounter.NextValue();
            return (int)usage;
        }


        private string GetInstanceName(Process process)
        {
            PerformanceCounterCategory category = new PerformanceCounterCategory("Process");
            string[] instances = category.GetInstanceNames();

            string instanceName = null;
            foreach (string instance in instances)
            {
                using (PerformanceCounter counter = new PerformanceCounter("Process",
                     "ID Process", instance, true))
                {
                    if ((int)counter.RawValue == process.Id)
                    {
                        instanceName = instance;
                        break;
                    }
                }
            }

            if (instanceName == null) throw new Exception("Process Instance PerformanceCounter not found for this Pid");;

            return instanceName;
        }
    }
}
