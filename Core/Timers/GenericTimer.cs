using System;
using System.Timers;

namespace ProcessGremlin.Core.Timers
{
    public class GenericTimer : IDisposable
    {
        private readonly Action _strategy;
        private readonly Timer _timer;

        public GenericTimer(Action strategy, double intervalMs)
        {
            _strategy = strategy;

            _timer = new Timer(intervalMs) {AutoReset = false};
            _timer.Elapsed += this.OnIntervalElapsed;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        public void Start()
        {
            Run();
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Run()
        {
            _strategy.Invoke();
        }

        private void OnIntervalElapsed(object sender, ElapsedEventArgs e)
        {
            Run();
            ((Timer) sender).Start();
        }
    }
}