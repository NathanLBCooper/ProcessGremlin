using System;
using System.Timers;

namespace ProcessGremlins
{
    public class GenericTimer : IDisposable
    {
        private readonly Action strategy; 
        private readonly Timer timer;

        public GenericTimer(Action strategy, double intervalMs)
        {
            this.strategy = strategy;

            this.timer = new Timer(intervalMs) { AutoReset = false };
            this.timer.Elapsed += this.OnIntervalElapsed;
        }

        public void Start()
        {
            this.Run();
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        public void Run()
        {
            this.strategy.Invoke();
        }

        private void OnIntervalElapsed(object sender, ElapsedEventArgs e)
        {
            this.Run();
            ((Timer)sender).Start();
        }

        public void Dispose()
        {
            this.timer.Dispose();
        }
    }
}
