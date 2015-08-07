using System;
using System.Collections.Generic;
using System.Diagnostics;

using ProcessGremlins;

namespace ProcessGremlinImplementations
{
    public class SimpleProcessGremlin : IGremlin
    {
        private readonly Action<IEnumerable<Process>> gremlinAction;
        private readonly IProcessFinder finder;

        public SimpleProcessGremlin(Action<IEnumerable<Process>> action, IProcessFinder processFinder)
        {
            this.gremlinAction = action;
            this.finder = processFinder;
        }

        public void Meddle()
        {
            this.gremlinAction.Invoke(this.finder.Find());
        }
    }
}