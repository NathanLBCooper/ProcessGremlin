namespace ProcessGremlinImplementations.Logging
{
    public interface IEventLogger
    {
        void Log(IEvent evt);
    }
}