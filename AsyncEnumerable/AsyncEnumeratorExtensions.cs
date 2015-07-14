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
        /// Calls a delegate on each element of an <see cref="IAsyncEnumerable{T}"/>
        /// </summary>
        /// <param name="source">A sequence of values to iterate.</param>
        /// <param name="act">The delegate to call</param>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <returns>Task that will finish when all the delegates are called</returns>
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
        /// Converts <see cref="IAsyncEnumerable{T}"/> to <see cref="IEnumerable{T}"/>
        ///     by awaiting on every moveNext one after another.
        /// </summary>
        /// <param name="source">A sequence of values to await.</param>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <returns>Task that will finish when all the elements of the source sequence arrive</returns>
        public static async Task<IEnumerable<T>> ToEnumerable<T>(this IAsyncEnumerable<T> source)
        {
            var result = new List<T>();
            var enumerator = source.GetEnumerator();
            while (await enumerator.MoveNext())
                result.Add(enumerator.Current);
            return result;
        }

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}"/>,  
        /// and flattens the resulting sequences into one sequence. 
        /// </summary>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each source element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the sequence returned by selector.</typeparam>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> whose elements are the result of 
        ///     invoking the one-to-many transform function on each element of the input sequence.</returns>
        public static IAsyncEnumerable<TResult> SelectManyAsync<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, Task<IEnumerable<TResult>>> selector)
        {
            return new SelectingManyAsyncEnumerable<TSource, TResult>(source, selector);
        }
        /// <summary>
        /// Returns a specified number of contiguous elements from the start of a sequence.
        /// </summary>
        /// <param name="source">The sequence to return elements from.</param>
        /// <param name="count"> The number of elements to return.</param>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> that contains the specified 
        ///     number of elements from the start of the input sequence.</returns>
        public static IAsyncEnumerable<T> Take<T>(
            this IAsyncEnumerable<T> source, int count)
        {
            return new TakeAsyncEnumerable<T>(source, count);
        }
    }
}
