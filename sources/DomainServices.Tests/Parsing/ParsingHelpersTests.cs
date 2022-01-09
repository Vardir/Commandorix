using System;

using FluentAssertions;

using Vardirsoft.Commandorix.DomainServices.Helpers;
using Vardirsoft.Commandorix.Entities.Commands;
using Vardirsoft.Commandorix.Entities.Parsing;
using Vardirsoft.Commandorix.Entities.Tokens;

using Xunit;

namespace DomainServices.Tests.Parsing;

public class ParsingHelpersTests
{
  [Fact]
  public void GetEmptyCommandOrError_WithAllowedEmptyCommands_ReturnsExpectedResult()
  {
    const ushort line = 1;
    const ushort position = 1;

    var result = ParsingHelpers.GetEmptyCommandOrError(true, line, position);

    result.Line.Should().Be(0);
    result.Location.Should().Be(0);
    result.IsSuccess.Should().BeTrue();
    result.LexingError.Should().Be(LexingError.None);
    result.ParsingError.Should().Be(ParsingError.None);
    result.Expression.Should().Be(EmptyExpression.Instance);
  }

  [Fact]
  public void GetEmptyCommandOrError_WithNotAllowedEmptyCommands_ReturnsExpectedResult()
  {
    const ushort line = 1;
    const ushort position = 12;

    var result = ParsingHelpers.GetEmptyCommandOrError(false, line, position);

    result.Line.Should().Be(line);
    result.Location.Should().Be(position);
    result.IsSuccess.Should().BeFalse();
    result.LexingError.Should().Be(LexingError.None);
    result.ParsingError.Should().Be(ParsingError.NoCommandFound);
    result.Invoking(r => r.Expression).Should().Throw<InvalidOperationException>();
  }
}