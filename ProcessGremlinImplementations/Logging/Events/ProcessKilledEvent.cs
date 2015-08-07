using System.Diagnostics;

using NLog;

namespace ProcessGremlinImplementations.Logging
{
    public sealed class ProcessKilledEvent : Event
    {
        public ProcessKilledEvent(Process process)
        {
            this.Detail = string.Format("Process {0} with pid {1} was Killed", process.ProcessName, process.Id);
            this.Level = LogLevel.Info;
            this.Name = "Process Killed";
        }
    }
}