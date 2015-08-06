using System.Collections.Generic;
using System.Diagnostics;

using ProcessGremlins;

namespace ProcessGremlinImplementations
{
    public class KillGremlin : IProcessGremlin
    {
        private readonly ProcessGremlin gremlin;

        public KillGremlin()
        {
            this.gremlin = new ProcessGremlin(
                processes =>
                {
                    foreach (var process in processes)
                    {
                        process.Kill();
                    }
                });
        }

        public void Invoke(IEnumerable<Process> data)
        {
            this.gremlin.Invoke(data);
        }
    }
}