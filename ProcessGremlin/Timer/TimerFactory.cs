namespace ProcessGremlins
{
    public class TimerFactory : ITimerFactory
    {
        public ITimer NewTimer(double intervalMs, bool autoReset)
        {
            return new Timer2(intervalMs) { AutoReset = false };
        }
    }
}
