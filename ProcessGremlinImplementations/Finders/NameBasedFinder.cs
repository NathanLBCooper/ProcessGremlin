using System.Collections.Generic;
using System.Diagnostics;
using ProcessGremlin.ProcessGremlin;

namespace ProcessGremlinImplementations.Finders
{
    public class NameBasedFinder : IProcessFinder
    {
        private readonly IEnumerable<string> names;

        public NameBasedFinder(IEnumerable<string> names)
        {
            this.names = names;
        }

        public NameBasedFinder(string name)
            : this(new[] {name})
        {
        }

        public IEnumerable<Process> Find()
        {
            var processes = new List<Process>();
            foreach (var name in this.names)
            {
                processes.AddRange(Process.GetProcessesByName(name));
            }

            return processes;
        }
    }
}