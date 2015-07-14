using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncEnumerable
{
    internal sealed class SelectingManyAsyncEnumerable<TArg, TResult> : IAsyncEnumerable<TResult>
    {
        private readonly IEnumerable<TArg> _source;
        private readonly Func<TArg, Task<IEnumerable<TResult>>> _selector;

        public SelectingManyAsyncEnumerable(IEnumerable<TArg> source, Func<TArg, Task<IEnumerable<TResult>>> selector)
        {
            _source = source;
            _selector = selector;
        }

        public IAsyncEnumerator<TResult> GetEnumerator()
        {
            return new SelectingManyAsyncEnumerator<TArg, TResult>(_source.GetEnumerator(), _selector);
        }
    }
}