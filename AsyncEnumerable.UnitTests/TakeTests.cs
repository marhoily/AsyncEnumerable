using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace SeqAsync.UnitTests
{
    public sealed class TakeTests
    {
        private readonly IAsyncEnumerable<int>
            _source = new[] {1, 2, 3}.SelectAsync(Task.FromResult);

        private readonly IAsyncEnumerable<int>
            _emptySource = new int[0].SelectAsync(Task.FromResult);


        [Fact]
        public async Task Take_4_of_3_Should_Work()
        {
            var enumerable = await _source.Take(4).ToEnumerable();
            enumerable.Should().Equal(1, 2, 3);
        }
        [Fact]
        public async Task Take_3_of_3_Should_Work()
        {
            var enumerable = await _source.Take(3).ToEnumerable();
            enumerable.Should().Equal(1, 2, 3);
        }
        [Fact]
        public async Task Take_2_of_3_Should_Work()
        {
            var enumerable = await _source.Take(2).ToEnumerable();
            enumerable.Should().Equal(1, 2);
        }
        [Fact]
        public async Task Take_1_of_3_Should_Work()
        {
            var enumerable = await _source.Take(1).ToEnumerable();
            enumerable.Should().Equal(1);
        }
        [Fact]
        public async Task Take_0_of_3_Should_Work()
        {
            var enumerable = await _source.Take(0).ToEnumerable();
            enumerable.Should().BeEmpty();
        }
        [Fact]
        public async Task Take_0_of_0_Should_Work()
        {
            var enumerable = await _emptySource.Take(0).ToEnumerable();
            enumerable.Should().BeEmpty();
        }
        [Fact]
        public async Task Take_1_of_0_Should_Work()
        {
            var enumerable = await _emptySource.Take(1).ToEnumerable();
            enumerable.Should().BeEmpty();
        }
    }
}