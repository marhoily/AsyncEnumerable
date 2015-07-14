using System.Threading.Tasks;

namespace SeqAsync
{
    internal sealed class TakeAsyncEnumerator<TArg> : IAsyncEnumerator<TArg>
    {
        private readonly IAsyncEnumerator<TArg> _asyncEnumerator;
        private int _itemsLeft;

        public TakeAsyncEnumerator(IAsyncEnumerator<TArg> asyncEnumerator, int itemsLeft)
        {
            _asyncEnumerator = asyncEnumerator;
            _itemsLeft = itemsLeft;
        }

        public void Dispose()
        {
            _asyncEnumerator.Dispose();
        }

        public async Task<bool> MoveNext()
        {
            if (--_itemsLeft < 0) return false;
            return await _asyncEnumerator.MoveNext();
        }

        public TArg Current { get { return _asyncEnumerator.Current; } }
    }
}