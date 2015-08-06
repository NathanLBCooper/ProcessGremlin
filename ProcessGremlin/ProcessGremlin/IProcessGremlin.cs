using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessGremlins
{
    public interface IProcessGremlin
    {
        void Invoke(IEnumerable<Process> data);
    }
}