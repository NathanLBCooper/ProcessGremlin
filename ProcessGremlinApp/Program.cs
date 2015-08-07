using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProcessGremlins;

using ProcessGremlinImplementations;
using ProcessGremlinImplementations.Logging;

namespace ProcessGremlinApp
{
    public class Program
    {
        // todo ioc
        private static readonly IEventLogger Logger = new EventLogger();

        static void Main(string[] args)
        {
            Logger.Log(new ApplicationStartedEvent());
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

            //todo ioc
            var finderBuilder = new ProcessFinderBuilder();
            var timerBuilder = new GremlinTimerBuilder();

            // todo arguments + config etc
            using (
                var gremlinTimer =
                    timerBuilder.CreateGremlinTimer(new KillBusyGremlin(finderBuilder.GetNameBasedFinder("notepad"), 70, Logger), 500)
                )
            {
                gremlinTimer.Start();
                Console.ReadKey();
            }
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Logger.Log(new ApplicationEndingEvent());
        }
    }
}
