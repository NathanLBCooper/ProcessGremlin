using System;
using System.Timers;

namespace ProcessGremlins
{
    public interface ITimer : IDisposable
    {
        bool AutoReset { get; set; }
        event ElapsedEventHandler Elapsed;
        void Start();
        void Stop();
    }

    // Force the System.Timers.Timer class into an interface
}