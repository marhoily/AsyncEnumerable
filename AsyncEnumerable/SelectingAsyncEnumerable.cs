using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncEnumerable
{
    internal sealed class SelectingAsyncEnumerable<TArg, TResult> : IAsyncEnumerable<TResult>
    {
        private readonly IEnumerable<TArg> _source;
        private readonly Func<TArg, Task<TResult>> _selector;

        public SelectingAsyncEnumerable(IEnumerable<TArg> source, Func<TArg, Task<TResult>> selector)
        {
            _source = source;
            _selector = selector;
        }

        public IAsyncEnumerator<TResult> GetEnumerator()
        {
            return new SelectingAsyncEnumerator<TArg, TResult>(_source.GetEnumerator(), _selector);
        }
    }
}