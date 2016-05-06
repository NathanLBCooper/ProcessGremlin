namespace ProcessGremlins
{
    public interface ITimerFactory
    {
        ITimer NewTimer(double intervalMs, bool autoReset);
    }
}