using FluentAssertions;
using Xunit;

namespace WordLadder.Tests
{
    public class StringExtensionsFixture
    {
        [Theory]
        [InlineData("one", "onf", true)] // substitution
        [InlineData("two", "tvwo", true)] // insertion
        [InlineData("two", "twoo", true)] // insertion (duplicate)
        [InlineData("two", "tw", true)] // deletion
        [InlineData("", "a", true)] // insertion to empty string
        [InlineData("a", "", true)] // deletion to empty string

        [InlineData("one", "one", false)] // no change
        [InlineData("one", "oneee", false)] // more than one insertion at end
        [InlineData("oneee", "one", false)] // more than one deletion at end
        [InlineData("one", "two", false)] // more than one substitution
        [InlineData("", "", false)] // empty strings

        [InlineData("moll", "mole", true)]
        [InlineData("moll", "toll", true)]
        [InlineData("moll", "atoll", false)]
        [InlineData("moll", "moe", false)]

        [InlineData("mall", "moyl", false)]
        public void IsAdjacentTest(string first, string second, bool isAdjacent)
        {
            first.IsAdjacent(second).Should().Be(isAdjacent);
        }
    }
}
