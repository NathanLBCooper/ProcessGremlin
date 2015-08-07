using ProcessGremlinImplementations;
using ProcessGremlinImplementations.Logging;

using ProcessGremlins;

namespace ProcessGremlinApp
{
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