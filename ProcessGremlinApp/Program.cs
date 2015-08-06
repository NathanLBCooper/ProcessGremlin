using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProcessGremlins;

using ProcessGremlinImplementations;

namespace ProcessGremlinApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var finders = new ProcessFinderBuilder();
            var timerBuilder = new GremlinTimerBuilder();

            //using (
            //    var gremlinTimer =
            //        timerBuilder.CreateGremlinTimer(
            //            finders.ConcatFinders(new[] { finders.GetNameBasedFinder("notepad"), finders.GetNameBasedFinder("python") }),
            //            new KillRandomGremlin(), 
            //            1000))
            //{
            //    gremlinTimer.Start();
            //    Console.ReadKey();
            //}

            using (
                var gremlinTimer =
                    timerBuilder.CreateGremlinTimer(
                        finders.ConcatFinders(new[] { finders.GetNameBasedFinder("notepad"), finders.GetNameBasedFinder("python"), finders.GetNameBasedFinder("cmd") }),
                        new EvenLoadPerNameGremlin(),
                        10000))
            {
                gremlinTimer.Start();
                Console.ReadKey();
            }
        }
    }
}
