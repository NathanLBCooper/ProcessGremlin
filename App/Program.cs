﻿using System;
using ProcessGremlin.Core.Timers;
using ProcessGremlin.Logging;
using ProcessGremlin.Logging.Events;

namespace ProcessGremlin.App
{
    /*
     * Example calls:
     * Kill -t 5000 -p notepad -n 1
     * KillBusy -t 5000 -p notepad -n 1 -c 100
    */

    public class Program
    {
        private static readonly IEventLogger Logger = new EventLogger();
        private static readonly Type Type = typeof (Program);

        private static void Main(string[] args)
        {
            Program.Logger.Log(new ApplicationStartedEvent(Program.Type));
            AppDomain.CurrentDomain.ProcessExit += Program.CurrentDomain_ProcessExit;
            AppDomain.CurrentDomain.UnhandledException += Program.CurrentDomain_UnhandledException;

            var timerBuilder = new GremlinTimerBuilder();

            var parser = new ArgumentParser();
            Arguments arguments;
            if (!parser.TryParse(args, out arguments))
            {
                Program.Logger.Log(new FailureEvent("Arguments do not parse", Program.Type));
                return;
            }

            Action gremlin;
            if (!arguments.TryBuildGremlin(Program.Logger, out gremlin))
            {
                Program.Logger.Log(new FailureEvent("Arguments do not parse", Program.Type));
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
            Program.Logger.Log(new ApplicationEndingEvent(Program.Type));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception exception = (Exception)args.ExceptionObject;
            Program.Logger.Log(
                new FailureEvent(string.Format("Unhandled Exception. Runtime terminating: {0}", args.IsTerminating),
                    exception, Program.Type));
        }
    }
}