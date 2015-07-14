namespace SeqAsync
{
    internal sealed class TakeAsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        private readonly IAsyncEnumerable<T> _source;
        private readonly int _count;

        public TakeAsyncEnumerable(IAsyncEnumerable<T> source, int count)
        {
            _source = source;
            _count = count;
        }

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new TakeAsyncEnumerator<T>(_source.GetEnumerator(), _count);
        }
    }
}