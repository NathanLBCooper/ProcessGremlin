namespace ProcessGremlin.Logging
{
    public interface IEventLogger
    {
        void Log(IEvent evt);
    }
}