using System;
using System.Collections.Generic;
using System.Diagnostics;
using ProcessGremlins;

namespace ProcessGremlinImplementations.GremlinStrategy
{
    public class SimpleProcessGremlin : IGremlin
    {
        private readonly IProcessFinder finder;
        private readonly Action<IEnumerable<Process>> gremlinAction;

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