using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using CommandLine.Text;

namespace ProcessGremlinApp
{
    public class Options
    {
        public const string KillBusyVerbStr = "KillBusy";
        public const string KillStr = "Kill";

        [VerbOption(KillBusyVerbStr, HelpText = "Kill Busy")]
        public KillBusySubOptions KillBusyVerb { get; set; }

        [VerbOption(KillStr, HelpText = "Kill")]
        public CommonOptions KillVerb { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
