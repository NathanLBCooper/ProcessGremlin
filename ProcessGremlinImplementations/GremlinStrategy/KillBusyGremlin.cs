﻿using System;
using System.Diagnostics;
using System.Linq;

using ProcessGremlins;

namespace ProcessGremlinImplementations
{
    public class KillBusyGremlin : IGremlin
    {
        private readonly float busyThreshold;
        private readonly IProcessFinder finder;

        // busyThreshold in % of core being used, ie up to 400 on a 4-core
        public KillBusyGremlin(IProcessFinder processFinder, float busyThreshold)
        {
            this.busyThreshold = busyThreshold;
            this.finder = processFinder;
        }

        public void Meddle()
        {
            var data = this.finder.Find();
            var busyProcesses = data.Where(process => this.GetCpuUsage(process) > this.busyThreshold).ToList();
            foreach (var process in busyProcesses)
            {
                Console.WriteLine("killing {0}", process.ProcessName); //todo nlog
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
