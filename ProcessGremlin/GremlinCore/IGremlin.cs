namespace ProcessGremlins
{
    public interface IGremlin<T>
    {
        void Invoke(T data);
    }
}