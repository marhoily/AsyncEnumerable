namespace SeqAsync
{
    public interface IAsyncEnumerable<out T>
    {
        IAsyncEnumerator<T> GetEnumerator();
    }
}