using System;
using System.Threading.Tasks;

namespace AsyncEnumerable
{
    /// <summary>
    /// Supports a asynchronous iteration over a generic collection.
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate.
    ///     This type parameter is covariant. 
    ///     That is, you can use either the type you specified or any type that is more derived. 
    ///     For more information about covariance and contravariance, 
    ///     see Covariance and Contravariance in Generics.</typeparam>
    public interface IAsyncEnumerator<out T> : IDisposable
    {
        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>true if the enumerator was successfully advanced to the next element; 
        /// false if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     The collection was modified after the enumerator was created.</exception>
        Task<bool> MoveNext();
        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        T Current { get; }
    }
}