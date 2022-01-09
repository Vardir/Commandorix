using System;

using FluentAssertions;

using Vardirsoft.Commandorix.DomainServices.Helpers;
using Vardirsoft.Commandorix.Entities.Tokens;

using Xunit;

namespace DomainServices.Tests.Parsing;

public class TokenSequenceTests
{
  [Theory]
  [InlineData(0, 1, true, false)]
  [InlineData(1, 3, true, true)]
  [InlineData(2, 3, true, false)]
  [InlineData(3, 0, false, true)]
  [InlineData(4, 0, false, false)]
  [InlineData(8, 0, false, false)]
  public void GetNextNonWhiteSpaceTokenPosition_DropAnywhereInSequence_ReturnsCorrectResults(ushort position, ushort expectedPosition, bool expectedToFind, bool anyWhiteSpacesInBetweenExpected)
  {
    var tokens = new Token[] { new (0, 0, 0, TokenKind.Asterisk), new (0, 0, 0, TokenKind.Backslash), new (0, 0, 0, TokenKind.Whitespace), new (0, 0, 0, TokenKind.Backslash), new (0, 0, 0, TokenKind.Whitespace) };

    var (found, anyWhiteSpaceInBetween, index) = TokenSequence.GetNextNonWhiteSpaceTokenPosition(tokens, position);

    found.Should().Be(expectedToFind);
    anyWhiteSpaceInBetween.Should().Be(anyWhiteSpacesInBetweenExpected);
    index.Should().Be(expectedPosition);
  }

  [Theory]
  [InlineData(true, TokenKind.EndOfFile)]
  [InlineData(true, TokenKind.EndOfLine)]
  [InlineData(false, TokenKind.EndOfFile)]
  [InlineData(false, TokenKind.EndOfLine)]
  public void GetNextNonWhiteSpaceTokenPosition_SequenceWithTerminator_ReturnsCorrectResults(bool shouldBreak, TokenKind terminator)
  {
    var tokens = new Token[] { new (0, 0, 0, TokenKind.Asterisk), new (0, 0, 0, terminator), new (0, 0, 0, TokenKind.Whitespace), new (0, 0, 0, TokenKind.Backslash), new (0, 0, 0, TokenKind.Whitespace) };

    var (found, anyWhiteSpaceInBetween, index) = TokenSequence.GetNextNonWhiteSpaceTokenPosition(tokens, 0, shouldBreak);

    //if search breaks on terminator we won't get to the backslash token nor to whitespace
    found.Should().NotBe(shouldBreak);
    anyWhiteSpaceInBetween.Should().NotBe(shouldBreak);
    index.Should().Be((ushort)(shouldBreak ? 0 : 3));
  }

  [Theory]
  [InlineData(0, true, TokenKind.Whitespace)]
  [InlineData(1, true, TokenKind.Backslash)]
  [InlineData(2, false, TokenKind.Unknown)]
  public void NextAfter_DropAnywhereInSequence_ReturnsCorrectResults(ushort position, bool expectedAny, TokenKind expectedToken)
  {
    var tokens = new Token[] { new (0, 0, 0, TokenKind.Asterisk), new (0, 0, 0, TokenKind.Whitespace), new (0, 0, 0, TokenKind.Backslash) };

    var (any, token) = TokenSequence.GetNextAfter(tokens, position);

    any.Should().Be(expectedAny);
    token.Kind.Should().Be(expectedToken);
  }

  [Fact]
  public void NextAfter_DropInEmptySequence_ReturnsCorrectResults()
  {
    var tokens = Array.Empty<Token>();

    var (any, token) = TokenSequence.GetNextAfter(tokens, 0);

    any.Should().BeFalse();
    token.Kind.Should().Be(TokenKind.Unknown);
  }
}