using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncEnumerable
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for 
    /// querying objects that implement <see cref="IEnumerable{T}"/>, 
    /// and <see cref="IAsyncEnumerable{T}"/>
    /// </summary>
    public static class AsyncEnumeratorExtensions
    {
        /// <summary>Projects each element of a sequence into a new form.</summary>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">An asinchronous transform function to apply to each element.</param>
        /// <typeparam name="TArg">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> whose elements are the result of invoking the transform function on each element of source.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
        public static IAsyncEnumerable<TResult> SelectAsync<TArg, TResult>(
            this IEnumerable<TArg> source, Func<TArg, Task<TResult>> selector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            return new SelectingAsyncEnumerable<TArg, TResult>(source, selector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="act"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task ForEachAsync<T>(
            this IAsyncEnumerable<T> source, Action<T> act)
        {
            var enumerator = source.GetEnumerator();
            while (await enumerator.MoveNext())
            {
                act(enumerator.Current);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ToEnumerable<T>(
            this IAsyncEnumerable<T> source)
        {
            var result = new List<T>();
            var enumerator = source.GetEnumerator();
            while (await enumerator.MoveNext())
                result.Add(enumerator.Current);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <typeparam name="TArg"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static IAsyncEnumerable<TResult> SelectManyAsync<TArg, TResult>(
            this IEnumerable<TArg> source,
            Func<TArg, Task<IEnumerable<TResult>>> selector)
        {
            return new SelectingManyAsyncEnumerable<TArg, TResult>(source, selector);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IAsyncEnumerable<T> Take<T>(
            this IAsyncEnumerable<T> source, int count)
        {
            return new TakeAsyncEnumerable<T>(source, count);
        }
    }
}
