using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using CommandLine.Text;

using ProcessGremlinImplementations;
using ProcessGremlinImplementations.Logging;

using ProcessGremlins;

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

    public class CommonOptions
    {
        [Option('t', "timerIntervalMs", Required = true, HelpText = "Timer Interval Ms")]
        public int TimerIntervalMs { get; set; }

        [Option('p', "process", Required = true, HelpText = "Process Name")]
        public string ProcessName { get; set; }
    }

    public class KillBusySubOptions : CommonOptions
    {
        [Option('c', "cpu", HelpText = "Cpu usage counted as busy, % of 1 core", Required = true)]
        public int CpuBusyThreshold { get; set; }        
    }

    public class ArgumentParser
    {
        public bool TryParse(string[] args, out Arguments arguments)
        {
            string invokedVerb = null;
            object invokedVerbInstance = null;

            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(
                args,
                options,
                (verb, subOptions) =>
                {
                    invokedVerb = verb;
                    invokedVerbInstance = subOptions;
                }) && invokedVerbInstance is CommonOptions)
            {
                arguments = new Arguments(invokedVerb, invokedVerbInstance);
                return true;
            }

            arguments = null;
            return false;
        }
    }

    public class Arguments
    {
        public string InvokedVerb { get; private set; }
        public object InvokedVerbInstance { get; private set; }

        public Arguments(string invokedVerb, object invokedVerbInstance)
        {
            this.InvokedVerb = invokedVerb;
            this.InvokedVerbInstance = invokedVerbInstance;
        }

        public bool TryBuildGremlin(ProcessFinderBuilder finderBuilder, IEventLogger logger, out IGremlin gremlin)
        {
            switch (this.InvokedVerb)
            {
                case Options.KillBusyVerbStr:
                {
                    var killOptions = (KillBusySubOptions)this.InvokedVerbInstance;
                    gremlin = new KillBusyGremlin(finderBuilder.GetNameBasedFinder(killOptions.ProcessName), killOptions.CpuBusyThreshold, logger);
                    return true;
                }
                case Options.KillStr:
                {
                    var killOptions = (CommonOptions)this.InvokedVerbInstance;
                    gremlin = new KillGremlin(finderBuilder.GetNameBasedFinder(killOptions.ProcessName), logger);
                    return true;
                }
            }

            gremlin = null;
            return false;
        }

        public int GetTimerIntervalMs()
        {
            return ((CommonOptions)this.InvokedVerbInstance).TimerIntervalMs;
        }
    }
}
