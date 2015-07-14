using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace AsyncEnumerable.UnitTests
{
    public sealed class SelectManyTests
    {
        [Fact]
        public async Task SelectMany_Should_Work()
        {
            var enumerable = await
                new[] { new[] { 1, 2 }, new[] { 3, 4 }, new[] { 5, 6 } }
                    .SelectManyAsync(x => Task.FromResult((IEnumerable<int>)x))
                    .ToEnumerable();
            enumerable.Should().Equal(1, 2, 3, 4, 5, 6);
        }

        [Fact]
        public async Task SelectMany_When_Empty_Batch_Should_Work()
        {
            var enumerable = await
                new[] { new[] { 1, 2 }, new int[0], new[] { 5, 6 } }
                    .SelectManyAsync(x => Task.FromResult((IEnumerable<int>)x))
                    .ToEnumerable();
            enumerable.Should().Equal(1, 2, 5, 6);
        }

        [Fact]
        public async Task SelectMany_When_Last_Batch_Empty_Should_Work()
        {
            var enumerable = await
                new[] { new[] { 1, 2 }, new[] { 5, 6 }, new int[0] }
                    .SelectManyAsync(x => Task.FromResult((IEnumerable<int>)x))
                    .ToEnumerable();
            enumerable.Should().Equal(1, 2, 5, 6);
        }

        [Fact]
        public async Task SelectMany_When_Null_Batch_Should_Throw_NullReferenceException()
        {
            await Assert.ThrowsAsync<NullReferenceException>(async () =>
                await new[] { new[] { 1, 2 }, null, new[] { 5, 6 } }
                    .SelectManyAsync(x => Task.FromResult((IEnumerable<int>)x))
                    .ToEnumerable());
        }

        [Fact]
        public async Task SelectMany_Should_Be_Lazy()
        {
            var counter = new int[1];
            var enumerable =
                new[] { 1, 2, 3 }
                    .SelectManyAsync(i => Task.FromResult((IEnumerable<int>)
                        new[] { ++counter[0], ++counter[0] }));
            var enumeratorAsync = enumerable.GetEnumerator();
            counter[0].Should().Be(0);
            await enumeratorAsync.MoveNext();
            counter[0].Should().Be(2);
        }
        
    }
}