using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProcessGremlin;

namespace ProcessGremlinApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var gremlins = new GremlinBuilder();
            var finders = new FinderBuilder();
            var timerBuilder = new GremlinTimerBuilder();

            var random = new Random();

            using (
                var gremlinTimer =
                    timerBuilder.CreateGremlinTimer(
                        finders.ConcatFinders(new[] { finders.GetNameBasedFinder("notepad"), finders.GetNameBasedFinder("python") }),
                        gremlins.KillRandom(random),
                        1000))
            {
                gremlinTimer.Start();
                Console.ReadKey();
            }
        }
    }
}
