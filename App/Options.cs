using CommandLine;
using CommandLine.Text;

namespace ProcessGremlin.App
{
    public class Options
    {
        public const string KillBusyVerbStr = "KillBusy";
        public const string KillStr = "Kill";

        [VerbOption(Options.KillBusyVerbStr, HelpText = "Kill Busy")]
        public KillBusySubOptions KillBusyVerb { get; set; }

        [VerbOption(Options.KillStr, HelpText = "Kill")]
        public CommonOptions KillVerb { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}