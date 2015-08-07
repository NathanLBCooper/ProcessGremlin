using System;
using ProcessGremlins;

using ProcessGremlinImplementations;
using ProcessGremlinImplementations.Logging;

namespace ProcessGremlinApp
{
    public class Program
    {
        private static readonly IEventLogger Logger = new EventLogger();

        static void Main(string[] args)
        {
            Logger.Log(new ApplicationStartedEvent());
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

            var finderBuilder = new ProcessFinderBuilder();
            var timerBuilder = new GremlinTimerBuilder();

            var parser = new ArgumentParser();
            Arguments arguments;
            if (!parser.TryParse(args, out arguments))
            {
                Logger.Log(new FailureEvent("Arguments do not parse"));
                return;
            }

            IGremlin gremlin;
            if (!arguments.TryBuildGremlin(finderBuilder, Logger, out gremlin))
            {
                Logger.Log(new FailureEvent("Arguments do not parse"));
                return;
            }

            using (
                var gremlinTimer =
                    timerBuilder.CreateGremlinTimer(gremlin, arguments.GetTimerIntervalMs())
                )
            {
                gremlinTimer.Start();
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Logger.Log(new ApplicationEndingEvent());
        }
    }
}
