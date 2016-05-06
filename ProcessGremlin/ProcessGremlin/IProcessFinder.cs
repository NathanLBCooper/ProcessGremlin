using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessGremlin.ProcessGremlin

{
    public interface IProcessFinder
    {
        IEnumerable<Process> Find();
    }
}