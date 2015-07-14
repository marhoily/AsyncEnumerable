using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeqAsync
{
    internal sealed class SelectingManyAsyncEnumerator<TArg, TResult> : IAsyncEnumerator<TResult>
    {
        private readonly IEnumerator<TArg> _sourceEnumerator;
        private IEnumerator<TResult> _currentBatch;
        private readonly Func<TArg, Task<IEnumerable<TResult>>> _selector;

        public SelectingManyAsyncEnumerator(IEnumerator<TArg> sourceEnumerator, Func<TArg, Task<IEnumerable<TResult>>> selector)
        {
            _sourceEnumerator = sourceEnumerator;
            _selector = selector;
        }

        public void Dispose()
        {
            _sourceEnumerator.Dispose();
        }

        public async Task<bool> MoveNext()
        {
            while(_currentBatch == null || !_currentBatch.MoveNext())
            {
                if (!_sourceEnumerator.MoveNext()) return false;
                var nextBatch = await _selector(_sourceEnumerator.Current);
                _currentBatch = nextBatch.GetEnumerator();
            }
            return true;
        }

        public TResult Current { get { return _currentBatch.Current; } }
    }
}