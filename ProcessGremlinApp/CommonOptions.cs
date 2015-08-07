using CommandLine;

namespace ProcessGremlinApp
{
    public class CommonOptions
    {
        [Option('t', "timerIntervalMs", Required = true, HelpText = "Timer Interval Ms")]
        public int TimerIntervalMs { get; set; }

        [Option('p', "process", Required = true, HelpText = "Process Name")]
        public string ProcessName { get; set; }
    }
}