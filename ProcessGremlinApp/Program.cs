﻿using System;
using ProcessGremlinImplementations.Logging;
using ProcessGremlinImplementations.Logging.Events;
using ProcessGremlins;

namespace ProcessGremlinApp
{
    public class Program
    {
        private static readonly IEventLogger Logger = new EventLogger();

        private static void Main(string[] args)
        {
            Program.Logger.Log(new ApplicationStartedEvent());
            AppDomain.CurrentDomain.ProcessExit += Program.CurrentDomain_ProcessExit;

            var timerBuilder = new GremlinTimerBuilder();

            var parser = new ArgumentParser();
            Arguments arguments;
            if (!parser.TryParse(args, out arguments))
            {
                Program.Logger.Log(new FailureEvent("Arguments do not parse"));
                return;
            }

            IGremlin gremlin;
            if (!arguments.TryBuildGremlin(Program.Logger, out gremlin))
            {
                Program.Logger.Log(new FailureEvent("Arguments do not parse"));
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

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Program.Logger.Log(new ApplicationEndingEvent());
        }
    }
}