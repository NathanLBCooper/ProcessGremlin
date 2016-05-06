using System.Collections.Generic;
using System.Diagnostics;
using ProcessGremlin.Core.Processes;

namespace ProcessGremlin.Implementations.Finders
{
    public class NameBasedFinder : IProcessFinder
    {
        private readonly IEnumerable<string> _names;

        public NameBasedFinder(IEnumerable<string> names)
        {
            _names = names;
        }

        public NameBasedFinder(string name)
            : this(new[] {name})
        {
        }

        public IEnumerable<Process> Find()
        {
            var processes = new List<Process>();
            foreach (var name in _names)
            {
                processes.AddRange(Process.GetProcessesByName(name));
            }

            return processes;
        }
    }
}