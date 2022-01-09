using System.Globalization;

using FluentAssertions;

using Vardirsoft.Commandorix.DomainServices.Parsing;

using Xunit;

namespace DomainServices.Tests.Parsing;

public class ValueParserTests
{
  [Theory]
  [InlineData("0", 0)]
  [InlineData("1", 1)]
  [InlineData("10", 10)]
  [InlineData("10.0", 10.0)]
  [InlineData("10.15", 10.15)]
  [InlineData("-10.15", -10.15)]
  public void TryParseNumber_ValidString_ReturnsNumber(string input, double expectedValue)
  {
    var parser = new ValueParser();

    var (success, value) = parser.TryParseNumber(input, CultureInfo.InvariantCulture);

    success.Should().BeTrue();
    value.Should().Be(expectedValue);
  }

  [Theory]
  [InlineData("")]
  [InlineData("*0")]
  [InlineData("1a0")]
  [InlineData("10/0")]
  [InlineData("10.15+8")]
  [InlineData("-10.1.5")]
  public void TryParseNumber_InvalidString_ReturnsFalse(string input)
  {
    var parser = new ValueParser();

    var (success, value) = parser.TryParseNumber(input, CultureInfo.InvariantCulture);

    success.Should().BeFalse();
    value.Should().Be(0.0d);
  }

  [Theory]
  [InlineData("0", false)]
  [InlineData("1", true)]
  [InlineData("true",true)]
  [InlineData("false",false)]
  [InlineData("True",true)]
  [InlineData("False",false)]
  public void TryParseBoolean_ValidString_ReturnsExpectedValue(string input, bool expectedValue)
  {
    var parser = new ValueParser();

    var (success, value) = parser.TryParseBoolean(input);

    success.Should().BeTrue();
    value.Should().Be(expectedValue);
  }

  [Theory]
  [InlineData("")]
  [InlineData("2")]
  [InlineData("-")]
  [InlineData("falsec")]
  [InlineData("0true")]
  public void TryParseBoolean_InvalidString_ReturnsFalse(string input)
  {
    var parser = new ValueParser();

    var (success, value) = parser.TryParseBoolean(input);

    success.Should().BeFalse();
    value.Should().Be(false);
  }
}