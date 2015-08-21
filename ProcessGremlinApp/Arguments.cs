using ProcessGremlinImplementations.Finders;
using ProcessGremlinImplementations.GremlinStrategy;
using ProcessGremlinImplementations.Logging;
using ProcessGremlins;

namespace ProcessGremlinApp
{
    public class Arguments
    {
        public Arguments(string invokedVerb, object invokedVerbInstance)
        {
            this.InvokedVerb = invokedVerb;
            this.InvokedVerbInstance = invokedVerbInstance;
        }

        public string InvokedVerb { get; private set; }
        public object InvokedVerbInstance { get; private set; }

        public bool TryBuildGremlin(IEventLogger logger, out IGremlin gremlin)
        {
            switch (this.InvokedVerb)
            {
                case Options.KillBusyVerbStr:
                {
                    var killOptions = (KillBusySubOptions) this.InvokedVerbInstance;
                    var finder = new BusyFinder(new NameBasedFinder(killOptions.ProcessName),
                        killOptions.CpuBusyThreshold);
                    gremlin = new KillGremlin(
                        killOptions.NumberToKill.HasValue
                            ? (IProcessFinder) new LimitedNumberFinder(finder, killOptions.NumberToKill.Value)
                            : finder, logger);
                    return true;
                }
                case Options.KillStr:
                {
                    var killOptions = (CommonOptions) this.InvokedVerbInstance;
                    var finder = new NameBasedFinder(killOptions.ProcessName);
                    gremlin =
                        new KillGremlin(
                            killOptions.NumberToKill.HasValue
                                ? (IProcessFinder) new LimitedNumberFinder(finder, killOptions.NumberToKill.Value)
                                : finder, logger);
                    return true;
                }
            }

            gremlin = null;
            return false;
        }

        public int GetTimerIntervalMs()
        {
            return ((CommonOptions) this.InvokedVerbInstance).TimerIntervalMs;
        }
    }
}