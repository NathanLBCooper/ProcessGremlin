using System;
using System.Diagnostics;
using NLog;

namespace Logging.Events
{
    public sealed class ProcessKilledEvent : Event
    {
        public ProcessKilledEvent(Process process, Type source)
            : base(source)
        {
            Detail = string.Format("Process {0} with pid {1} was Killed", process.ProcessName, process.Id);
            Level = LogLevel.Info;
            Name = "Process Killed";
        }
    }
}