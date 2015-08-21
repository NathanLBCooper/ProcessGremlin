using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using ProcessGremlinImplementations.Logging;
using ProcessGremlinImplementations.Logging.Events;
using ProcessGremlins;

namespace ProcessGremlinImplementations.Finders
{
    public class BusyFinder : IProcessFinder
    {
        private const int SampleTime = 1000;
        private readonly int busyThreshold;
        private readonly IProcessFinder finder;
        private readonly IEventLogger logger;

        // busyThreshold in % of core being used, ie up to 400 on a 4-core
        public BusyFinder(IProcessFinder finder, int busyThreshold, IEventLogger logger)
        {
            this.finder = finder;
            this.busyThreshold = busyThreshold;
            this.logger = logger;
        }

        // Expensive operations here, evaluates lazily
        public IEnumerable<Process> Find()
        {
            var data = this.finder.Find();
            return data.Where(process => this.GetCpuUsage(process) > this.busyThreshold);
        }

        private int GetCpuUsage(Process process)
        {
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", this.GetInstanceName(process), true);
            this.logger.Log(new IntervalStartingEvent("Beginning to measure CPU usage"));
            cpuCounter.NextValue();
            Thread.Sleep(BusyFinder.SampleTime);
            var usage = (int)cpuCounter.NextValue();
            this.logger.Log(new MeasuredCpuEvent(process, usage, BusyFinder.SampleTime));
            this.logger.Log(new IntervalStartingEvent("Ending measure of CPU usage"));
            return usage;
        }

        private string GetInstanceName(Process process)
        {
            var category = new PerformanceCounterCategory("Process");
            var instances = category.GetInstanceNames();

            string instanceName = null;
            foreach (var instance in instances)
            {
                using (var counter = new PerformanceCounter("Process",
                    "ID Process", instance, true))
                {
                    try
                    {
                        if ((int) counter.RawValue == process.Id)
                        {
                            instanceName = instance;
                            break;
                        }
                    }
                    catch(Exception exception)
                    {
                        // Suppress errors from instances without counters
                        this.logger.Log(new WarningEvent(exception));
                    }
                }
            }

            if (instanceName == null) throw new Exception("Process Instance PerformanceCounter not found for this Pid");
            ;

            return instanceName;
        }
    }
}