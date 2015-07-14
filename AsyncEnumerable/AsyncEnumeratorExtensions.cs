using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncEnumerable
{
    public static class AsyncEnumeratorExtensions
    {
        public static IAsyncEnumerable<TResult> SelectAsync<TArg, TResult>(
            this IEnumerable<TArg> source, Func<TArg, Task<TResult>> selector)
        {
            return new SelectingAsyncEnumerable<TArg, TResult>(source, selector);
        }

        public static async Task ForEachAsync<T>(
            this IAsyncEnumerable<T> source, Action<T> act)
        {
            var enumerator = source.GetEnumerator();
            while (await enumerator.MoveNext())
            {
                act(enumerator.Current);
            }
        }
        public static async Task<IEnumerable<T>> ToEnumerable<T>(
            this IAsyncEnumerable<T> source)
        {
            var result = new List<T>();
            var enumerator = source.GetEnumerator();
            while (await enumerator.MoveNext())
                result.Add(enumerator.Current);
            return result;
        }

        public static IAsyncEnumerable<TResult> SelectManyAsync<TArg, TResult>(
            this IEnumerable<TArg> source,
            Func<TArg, Task<IEnumerable<TResult>>> selector)
        {
            return new SelectingManyAsyncEnumerable<TArg, TResult>(source, selector);
        }
        public static IAsyncEnumerable<T> Take<T>(
            this IAsyncEnumerable<T> source, int count)
        {
            return new TakeAsyncEnumerable<T>(source, count);
        }
    }
}
