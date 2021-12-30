using FluentAssertions;

using Vardirsoft.Commandorix.Utils.Helpers;

using Xunit;

namespace Utils.Tests.Helpers;

public class CharSequenceTests
{
  [Theory]
  [InlineData("1:::x", ':', 4, VerbalCount.Odd)]
  [InlineData("1::::x", ':', 5, VerbalCount.Even)]
  [InlineData("19605x", ':', 5, VerbalCount.None)]
  [InlineData("19605x", ':', 0, VerbalCount.None)]
  public void CountLeadingCharsVerbally(string input, char character, ushort position, VerbalCount expectedCount)
  {
    var result = CharSequence.CountLeadingCharsVerbally(input, character, position);

    result.Should().Be(expectedCount);
  }

  [Theory]
  [InlineData("1:::x", ':', 4, 3)]
  [InlineData("1::::x", ':', 5, 4)]
  [InlineData("19605x", ':', 5, 0)]
  [InlineData("19605x", ':', 0, 0)]
  public void CountLeadingChars(string input, char character, ushort position, ushort expectedCount)
  {
    var result = CharSequence.CountLeadingChars(input, character, position);

    result.Should().Be(expectedCount);
  }
}