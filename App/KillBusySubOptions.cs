using CommandLine;

namespace ProcessGremlin.App
{
    public class KillBusySubOptions : CommonOptions
    {
        [Option('c', "cpu", HelpText = "Cpu usage counted as busy, % of 1 core", Required = true)]
        public int CpuBusyThreshold { get; set; }
    }
}