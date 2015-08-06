using System;
using System.Collections.Generic;
using System.Diagnostics;

using ProcessGremlins;

namespace ProcessGremlinImplementations
{
    public class SimpleProcessGremlin : IProcessGremlin
    {
        private readonly Action<IEnumerable<Process>> gremlinAction;

        public SimpleProcessGremlin(Action<IEnumerable<Process>> action)
        {
            this.gremlinAction = action;
        }

        public void Invoke(IEnumerable<Process> data)
        {
            this.gremlinAction.Invoke(data);
        }
    }
}