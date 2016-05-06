using System;
using System.Diagnostics;
using NLog;

namespace Logging.Events
{
    public sealed class MeasuredCpuEvent : Event
    {
        public MeasuredCpuEvent(Process process, int usage, int sampleTimeMs, Type source)
            : base(source)
        {
            Detail = string.Format("Process {0} with Id {1} measured as {2} cpu utilisation (Sample time of {3} ms)",
                process.ProcessName, process.Id, usage, sampleTimeMs);
            Level = LogLevel.Debug;
            Name = "Interval Ending";
        }
    }
}