using ProcessGremlins;

namespace ProcessGremlinImplementations
{
    public class KillGremlin : IGremlin
    {
        private readonly SimpleProcessGremlin gremlin;

        public KillGremlin(IProcessFinder processFinder)
        {
            this.gremlin = new SimpleProcessGremlin(
                processes =>
                {
                    foreach (var process in processes)
                    {
                        process.Kill();
                    }
                }, processFinder);
        }

        public void Invoke()
        {
            this.gremlin.Invoke();
        }
    }
}