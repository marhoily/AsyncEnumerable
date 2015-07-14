using System;
using System.Threading.Tasks;

namespace AsyncEnumerable
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncEnumerator<out T> : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> MoveNext();
        /// <summary>
        /// 
        /// </summary>
        T Current { get; }
    }
}