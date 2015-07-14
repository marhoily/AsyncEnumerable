using System;
using System.Threading.Tasks;

namespace SeqAsync
{
    public interface IAsyncEnumerator<out T> : IDisposable
    {
        Task<bool> MoveNext();
        T Current { get; }
    }
}