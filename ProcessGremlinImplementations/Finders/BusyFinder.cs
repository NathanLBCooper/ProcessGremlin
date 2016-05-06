using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using ProcessGremlin.ProcessGremlin;
using ProcessGremlinImplementations.Logging;
using ProcessGremlinImplementations.Logging.Events;

namespace ProcessGremlinImplementations.Finders
{
    public class BusyFinder : IProcessFinder
    {
        private const int SampleTime = 1000;
        private readonly int _busyThreshold;
        private readonly IProcessFinder _finder;
        private readonly IEventLogger _logger;
        private static readonly Type Type = typeof(BusyFinder);

        // _busyThreshold in % of core being used, ie up to 400 on a 4-core
        public BusyFinder(IProcessFinder finder, int busyThreshold, IEventLogger logger)
        {
            _finder = finder;
            _busyThreshold = busyThreshold;
            _logger = logger;
        }

        // Expensive operations here, evaluates lazily
        public IEnumerable<Process> Find()
        {
            var data = _finder.Find();
            return data.Where(process => GetCpuUsage(process) > _busyThreshold);
        }

        private int GetCpuUsage(Process process)
        {
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", GetInstanceName(process), true);
            _logger.Log(new IntervalStartingEvent("Beginning to measure CPU usage", BusyFinder.Type));
            cpuCounter.NextValue();
            Thread.Sleep(BusyFinder.SampleTime);
            var usage = (int)cpuCounter.NextValue();
            _logger.Log(new MeasuredCpuEvent(process, usage, BusyFinder.SampleTime, Type));
            _logger.Log(new IntervalStartingEvent("Ending measure of CPU usage", BusyFinder.Type));
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
                        _logger.Log(new WarningEvent(exception, BusyFinder.Type));
                    }
                }
            }

            if (instanceName == null) throw new Exception("Process Instance PerformanceCounter not found for this Pid");
            ;

            return instanceName;
        }
    }
}