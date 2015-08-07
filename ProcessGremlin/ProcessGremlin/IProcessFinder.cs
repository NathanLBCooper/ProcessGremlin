using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessGremlins
{
    public interface IProcessFinder
    {
        IEnumerable<Process> Find();
    }
}
