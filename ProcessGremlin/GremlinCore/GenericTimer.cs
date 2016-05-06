using System;
using System.Timers;

namespace ProcessGremlins
{
    public class GenericTimer : IDisposable
    {
        private readonly Action strategy;
        private readonly ITimer timer;

        public GenericTimer(Action strategy, double intervalMs, ITimerFactory timerFactory)
        {
            this.strategy = strategy;

            this.timer = timerFactory.NewTimer(intervalMs, false);
            this.timer.Elapsed += this.OnIntervalElapsed;
        }

        public void Dispose()
        {
            this.timer.Dispose();
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

            // Manually start (rather than use autoreset) as Run may be slow
            timer.Start();
        }
    }
}