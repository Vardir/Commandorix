using FluentAssertions;

using Vardirsoft.Commandorix.DomainServices.Lexing;
using Vardirsoft.Commandorix.DomainServices.Parsing;
using Vardirsoft.Commandorix.Entities.Tokens;

using Xunit;

namespace DomainServices.Tests.Lexing;

public sealed class StringLexerTests
{
  [Theory]
  [InlineData("string", TokenKind.StringLiteral)]
  [InlineData("1.0", TokenKind.StringLiteral)]
  [InlineData(".0", TokenKind.StringLiteral)]
  [InlineData("'string'", TokenKind.QuotedStringLiteral)]
  [InlineData("\"string\"", TokenKind.DoubleQuotedStringLiteral)]
  public void Tokenize_ValidLineWithString_ReturnsExpectedToken(string input, TokenKind expectedKind)
  {
    var stringLexer = new StringLexer();

    var expectedToken = new Token(1, 0, (ushort)input.Length, expectedKind);

    var result = stringLexer.Tokenize(input, 1, 0);

    result.Should().BeEquivalentTo(expectedToken);
  }

  [Theory]
  [InlineData(@"'quoted s\\\'tring'", TokenKind.QuotedStringLiteral)]
  [InlineData("\"quoted s" + @"\\\" + "\"tring\"", TokenKind.DoubleQuotedStringLiteral)]
  public void Tokenize_ValidLineWithStringWithEscapedQuote_ReturnsExpectedToken(string input, TokenKind tokenKind)
  {
    var stringLexer = new StringLexer();

    var expectedToken = new Token(1, 0, (ushort)input.Length, tokenKind);

    var result = stringLexer.Tokenize(input, 1, 0);

    result.Should().BeEquivalentTo(expectedToken);
  }

  [Theory]
  [InlineData(@"'quoted s\\\\'tring'", TokenKind.QuotedStringLiteral)]
  [InlineData("\"quoted s" + @"\\\\" + "\"tring\"", TokenKind.DoubleQuotedStringLiteral)]
  public void Tokenize_ValidLineWithStringWithNonEscapedQuote_ReturnsExpectedToken(string input, TokenKind tokenKind)
  {
    var stringLexer = new StringLexer();

    var expectedToken = new Token(1, 0, 14, tokenKind);

    var result = stringLexer.Tokenize(input, 1, 0);

    result.Should().BeEquivalentTo(expectedToken);
  }

  [Theory]
  [InlineData("''", TokenKind.QuotedStringLiteral)]
  [InlineData("\"\"", TokenKind.DoubleQuotedStringLiteral)]
  public void Tokenize_ValidLineWithEmptyString_ReturnsExpectedToken(string input, TokenKind tokenKind)
  {
    var stringLexer = new StringLexer();

    var expectedToken = new Token(1, 0, 2, tokenKind);

    var result = stringLexer.Tokenize(input, 1, 0);

    result.Should().BeEquivalentTo(expectedToken);
  }

  [Theory]
  [InlineData("'", TokenKind.QuotedStringLiteral)]
  [InlineData("\"", TokenKind.DoubleQuotedStringLiteral)]
  [InlineData("'some string", TokenKind.QuotedStringLiteral)]
  [InlineData("\"some string", TokenKind.DoubleQuotedStringLiteral)]
  public void Tokenize_NonClosedLine_ReturnsExpectedTokenWithError(string input, TokenKind tokenKind)
  {
    var stringLexer = new StringLexer();

    var expectedToken = new Token(1, 0, (ushort)input.Length, tokenKind, LexingError.StringLiteralIsNotClosed);

    var result = stringLexer.Tokenize(input, 1, 0);

    result.Should().BeEquivalentTo(expectedToken);
  }
}