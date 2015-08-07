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
            var finderBuilder = new ProcessFinderBuilder();
            var timerBuilder = new GremlinTimerBuilder();

            using (
                var gremlinTimer =
                    timerBuilder.CreateGremlinTimer(new EvenLoadPerNameGremlin(finderBuilder.GetMultipleNameFinders(new[] { "notepad", "python", "cmd" })), 500)
                )
            {
                gremlinTimer.Start();
                Console.ReadKey();
            }
        }
    }
}
