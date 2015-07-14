using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace SeqAsync.UnitTests
{
    public sealed class ToEnumerableTests
    {
        private readonly IAsyncEnumerable<int>
            _source = new[] { 1, 2, 3 }.SelectAsync(Task.FromResult);
        private readonly IAsyncEnumerable<int>
            _emptySource = new int[0].SelectAsync(Task.FromResult);


        [Fact]
        public async Task ToEnumerable_Should_Work()
        {
            var enumerable = await _source.ToEnumerable();
            enumerable.Should().Equal(1, 2, 3);
        }

        [Fact]
        public async Task ToEnumerable_When_Empty_Should_Work()
        {
            var enumerable = await _emptySource.ToEnumerable();
            enumerable.Should().BeEmpty();
        }
    }
}