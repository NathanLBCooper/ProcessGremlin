using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProcessGremlins;

namespace ProcessGremlinImplementations.GremlinStrategy
{
    //todo seems to kill all
    public class KillBusyGremlin : IProcessGremlin
    {
        private readonly float busyThreshold;
        private readonly object locker = new object();

        // busyThreshold in % of core being used, ie up to 400 on a 4-core
        public KillBusyGremlin(float busyThreshold)
        {
            this.busyThreshold = busyThreshold;
        }

        public void Invoke(IEnumerable<Process> data)
        {
            var busyProcesses = data.Where(process => this.GetCpuUsage(process) > this.busyThreshold).ToList();
            foreach (var process in busyProcesses)
            {
                process.Kill();
            }
        }

        private float GetCpuUsage(Process process)
        {
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", this.GetInstanceName(process), true);
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            var usage = cpuCounter.NextValue();
            return usage;
        }


        private string GetInstanceName(Process process)
        {
            PerformanceCounterCategory category = new PerformanceCounterCategory("Process");
            string[] instances = category.GetInstanceNames();

            string instanceName = null;
            foreach (string instance in instances)
            {
                using (PerformanceCounter cnt = new PerformanceCounter("Process",
                     "ID Process", instance, true))
                {
                    int val = (int)cnt.RawValue;
                    if (val == process.Id)
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
