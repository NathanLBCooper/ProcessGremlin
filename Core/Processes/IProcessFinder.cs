using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessGremlin.Core.Processes

{
    public interface IProcessFinder
    {
        IEnumerable<Process> Find();
    }
}