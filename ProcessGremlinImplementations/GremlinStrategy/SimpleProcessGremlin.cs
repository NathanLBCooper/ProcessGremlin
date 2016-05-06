using System;
using System.Collections.Generic;
using System.Diagnostics;
using ProcessGremlin.ProcessGremlin;

namespace ProcessGremlinImplementations.GremlinStrategy
{
    public class SimpleProcessGremlin
    {
        private readonly IProcessFinder _finder;
        private readonly Action<IEnumerable<Process>> _gremlinAction;

        public SimpleProcessGremlin(Action<IEnumerable<Process>> action, IProcessFinder processFinder)
        {
            _gremlinAction = action;
            _finder = processFinder;
        }

        public void Meddle()
        {
            _gremlinAction.Invoke(_finder.Find());
        }
    }
}