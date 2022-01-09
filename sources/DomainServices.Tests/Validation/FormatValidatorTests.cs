using System.Linq;

using FluentAssertions;

using Vardirsoft.Commandorix.DomainServices.Validation;

using Xunit;

namespace DomainServices.Tests.Validation;

public class FormatValidatorTests
{
  [Theory]
  [InlineData("a")]
  [InlineData("ab")]
  [InlineData("ab-c")]
  [InlineData("ab_c")]
  [InlineData("_ab_c_")]
  [InlineData("ab1c2")]
  public void ValidateVariableIdentifier_ValidInput_ReturnsTrue(string input)
  {
    var validator = new FormatValidator();

    var result = validator.ValidateVariableIdentifier(input);

    result.Should().BeTrue();
  }

  [Theory]
  [InlineData("")]
  [InlineData("-ab")]
  [InlineData("ab-")]
  [InlineData("1ab")]
  [InlineData("a$b")]
  public void ValidateVariableIdentifier_InvalidValidInput_ReturnsFalse(string input)
  {
    var validator = new FormatValidator();

    var result = validator.ValidateVariableIdentifier(input);

    result.Should().BeFalse();
  }

  [Theory]
  [InlineData("a")]
  [InlineData("ab")]
  [InlineData("ab-c")]
  [InlineData("ab_c")]
  [InlineData("_ab_c_")]
  [InlineData("ab1c2")]
  public void ValidateCommandIdentifier_ValidInput_ReturnsTrue(string input)
  {
    var validator = new FormatValidator();

    var result = validator.ValidateCommandIdentifier(input);

    result.Should().BeTrue();
  }

  [Theory]
  [InlineData("")]
  [InlineData("-ab")]
  [InlineData("ab-")]
  [InlineData("1ab")]
  [InlineData("a$b")]
  public void ValidateCommandIdentifier_InvalidValidInput_ReturnsFalse(string input)
  {
    var validator = new FormatValidator();

    var result = validator.ValidateCommandIdentifier(input);

    result.Should().BeFalse();
  }

  [Theory]
  [InlineData("ab")]
  [InlineData("ab-c")]
  [InlineData("ab_c")]
  [InlineData("_ab_c_")]
  [InlineData("ab1c2")]
  public void ValidateCommandParameter_ValidInput_ReturnsTrue(string input)
  {
    var validator = new FormatValidator();

    var result = validator.ValidateCommandParameter(input);

    result.Should().BeTrue();
  }

  [Theory]
  [InlineData("")]
  [InlineData("a")]
  [InlineData("-ab")]
  [InlineData("ab-")]
  [InlineData("1ab")]
  [InlineData("a$b")]
  public void ValidateCommandParameter_InvalidValidInput_ReturnsFalse(string input)
  {
    var validator = new FormatValidator();

    var result = validator.ValidateCommandParameter(input);

    result.Should().BeFalse();
  }

  [Fact]
  public void ValidateCommandParameterShortcut_ValidInput_ReturnsTrue()
  {
    var validator = new FormatValidator();
    var letters = Enumerable.Range('a', 26).Select(c => ((char)c).ToString()).ToArray();

    foreach (var letter in letters)
    {
      var result = validator.ValidateCommandParameterShortcut(letter);

      result.Should().BeTrue($"Letter {letter} should be valid");
    }
  }

  [Theory]
  [InlineData("")]
  [InlineData("-")]
  [InlineData("7")]
  [InlineData("*")]
  [InlineData("_")]
  [InlineData("$")]
  public void ValidateCommandParameterShortcut_InvalidValidInput_ReturnsFalse(string input)
  {
    var validator = new FormatValidator();

    var result = validator.ValidateCommandParameterShortcut(input);

    result.Should().BeFalse();
  }
}