using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeqAsync
{
    internal sealed class SelectingAsyncEnumerator<TArg, TResult> : IAsyncEnumerator<TResult>
    {
        private readonly IEnumerator<TArg> _enumerator;
        private readonly Func<TArg, Task<TResult>> _selector;

        public SelectingAsyncEnumerator(IEnumerator<TArg> enumerator, Func<TArg, Task<TResult>> selector)
        {
            _enumerator = enumerator;
            _selector = selector;
        }

        public void Dispose()
        {
            _enumerator.Dispose();
        }

        public async Task<bool> MoveNext()
        {
            if (!_enumerator.MoveNext()) return false;
            Current = await _selector(_enumerator.Current);
            return true;
        }

        public TResult Current { get; private set; }
    }
}