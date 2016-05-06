using System.Timers;

namespace ProcessGremlins
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class Timer2 : Timer, ITimer
    {
        public Timer2(double intervalMs) : base(intervalMs)
        {
        }
    }
}