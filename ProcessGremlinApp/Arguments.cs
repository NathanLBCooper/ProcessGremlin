using System;
using ProcessGremlin.ProcessGremlin;
using ProcessGremlinImplementations.Finders;
using ProcessGremlinImplementations.GremlinStrategy;
using ProcessGremlinImplementations.Logging;

namespace ProcessGremlinApp
{
    public class Arguments
    {
        public string InvokedVerb { get; private set; }
        public object InvokedVerbInstance { get; private set; }

        public Arguments(string invokedVerb, object invokedVerbInstance)
        {
            InvokedVerb = invokedVerb;
            InvokedVerbInstance = invokedVerbInstance;
        }

        public bool TryBuildGremlin(IEventLogger logger, out Action gremlin)
        {
            switch (InvokedVerb)
            {
                case Options.KillBusyVerbStr:
                {
                    var killOptions = (KillBusySubOptions) InvokedVerbInstance;
                    var finder = new BusyFinder(new NameBasedFinder(killOptions.ProcessName),
                        killOptions.CpuBusyThreshold, logger);
                    gremlin = new KillGremlin(
                        killOptions.NumberToKill.HasValue
                            ? (IProcessFinder) new LimitedNumberFinder(finder, killOptions.NumberToKill.Value)
                            : finder, logger).Meddle;
                    return true;
                }
                case Options.KillStr:
                {
                    var killOptions = (CommonOptions) InvokedVerbInstance;
                    var finder = new NameBasedFinder(killOptions.ProcessName);
                    gremlin =
                        new KillGremlin(
                            killOptions.NumberToKill.HasValue
                                ? (IProcessFinder) new LimitedNumberFinder(finder, killOptions.NumberToKill.Value)
                                : finder, logger).Meddle;
                    return true;
                }
            }

            gremlin = null;
            return false;
        }

        public int GetTimerIntervalMs()
        {
            return ((CommonOptions) InvokedVerbInstance).TimerIntervalMs;
        }
    }
}