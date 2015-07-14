namespace AsyncEnumerable
{
    /// <summary>
    /// Exposes the enumerator, which supports a asynchronous iteration over a collection of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate. This type parameter is covariant.
    ///   That is, you can use either the type you specified or any type that is more derived. 
    ///   For more information about covariance and contravariance, see Covariance and Contravariance in Generics.
    /// </typeparam>
    public interface IAsyncEnumerable<out T>
    {
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="IAsyncEnumerator{T}"/> 
        ///     that can be used to iterate through the collection.</returns>
        IAsyncEnumerator<T> GetEnumerator();
    }
}