using FluentAssertions;

using NSubstitute;

using Vardirsoft.Commandorix.DomainServices.Parsing;
using Vardirsoft.Commandorix.Entities.Parsing;

using Xunit;

namespace DomainServices.Tests.Parsing;

public sealed class LineLexerTests
{
  [Fact]
  public void Tokenize_ValidLine_ReturnsExpectedTokens()
  {
    var stringLexer = Substitute.For<IStringLexer>();
    var lexer = new LineLexer(stringLexer);

    const string line = "  () { } [  ] $ - ;,\\\t=  :+ * #%";
    var expectedTokens = new Token[]
    {
      new(1,0,2, TokenKind.Whitespace),
      new(1,2,1, TokenKind.OpenBracket),
      new(1,3,1, TokenKind.CloseBracket),
      new(1,4,1, TokenKind.Whitespace),
      new(1,5,1, TokenKind.OpenCurlyBracket),
      new(1,6,1, TokenKind.Whitespace),
      new(1,7,1, TokenKind.CloseCurlyBracket),
      new(1,8,1, TokenKind.Whitespace),
      new(1,9,1, TokenKind.OpenSquareBracket),
      new(1,10,2, TokenKind.Whitespace),
      new(1,12,1, TokenKind.CloseSquareBracket),
      new(1,13,1, TokenKind.Whitespace),
      new(1,14,1, TokenKind.DollarSign),
      new(1,15,1, TokenKind.Whitespace),
      new(1,16,1, TokenKind.Dash),
      new(1,17,1, TokenKind.Whitespace),
      new(1,18,1, TokenKind.Semicolon),
      new(1,19,1, TokenKind.Comma),
      new(1,20,1, TokenKind.Backslash),
      new(1,21,1, TokenKind.Whitespace),
      new(1,22,1, TokenKind.EqualSign),
      new(1,23,2, TokenKind.Whitespace),
      new(1,25,1, TokenKind.Colon),
      new(1,26,1, TokenKind.Plus),
      new(1,27,1, TokenKind.Whitespace),
      new(1,28,1, TokenKind.Asterisk),
      new(1,29,1, TokenKind.Whitespace),
      new(1,30,1, TokenKind.Hash),
      new(1,31,1, TokenKind.Percent),
      new(1,32,0, TokenKind.EndOfLine)
    };

    var result = lexer.Tokenize(line, 1);

    result.Success.Should().BeTrue();
    result.IsEmpty.Should().BeFalse();
    result.Tokens.Should().BeEquivalentTo(expectedTokens);
  }

  [Fact]
  public void Tokenize_ValidLineWithQuotedStrings_ReturnsExpectedTokens()
  {
    var lexer = new LineLexer(new StringLexer());

    const string line = "  () { } [ \"[]-string\" 'string' -s()t'ri=+n\\g \t  ] $ - ;,\\\t=";
    var expectedTokens = new Token[]
    {
      new(1,0,2, TokenKind.Whitespace),
      new(1,2,1, TokenKind.OpenBracket),
      new(1,3,1, TokenKind.CloseBracket),
      new(1,4,1, TokenKind.Whitespace),
      new(1,5,1, TokenKind.OpenCurlyBracket),
      new(1,6,1, TokenKind.Whitespace),
      new(1,7,1, TokenKind.CloseCurlyBracket),
      new(1,8,1, TokenKind.Whitespace),
      new(1,9,1, TokenKind.OpenSquareBracket),
      new(1,10,1, TokenKind.Whitespace),
      new(1,11,11, TokenKind.DoubleQuotedStringLiteral),
      new(1,22,1, TokenKind.Whitespace),
      new(1,23,8, TokenKind.QuotedStringLiteral),
      new(1,31,1, TokenKind.Whitespace),
      new(1,32,1, TokenKind.Dash),
      new(1,33,12, TokenKind.StringLiteral),
      new(1,45,4, TokenKind.Whitespace),
      new(1,49,1, TokenKind.CloseSquareBracket),
      new(1,50,1, TokenKind.Whitespace),
      new(1,51,1, TokenKind.DollarSign),
      new(1,52,1, TokenKind.Whitespace),
      new(1,53,1, TokenKind.Dash),
      new(1,54,1, TokenKind.Whitespace),
      new(1,55,1, TokenKind.Semicolon),
      new(1,56,1, TokenKind.Comma),
      new(1,57,1, TokenKind.Backslash),
      new(1,58,1, TokenKind.Whitespace),
      new(1,59,1, TokenKind.EqualSign),
      new(1,60,0, TokenKind.EndOfLine)
    };

    var result = lexer.Tokenize(line, 1);

    result.Success.Should().BeTrue();
    result.IsEmpty.Should().BeFalse();
    result.Tokens.Should().BeEquivalentTo(expectedTokens);
  }

  [Fact]
  public void Tokenize_ValidLineWithEOL_ReturnsExpectedTokens()
  {
    var stringLexer = Substitute.For<IStringLexer>();
    var lexer = new LineLexer(stringLexer);

    const string line = "  ()\n";
    var expectedTokens = new Token[]
    {
      new(1,0,2, TokenKind.Whitespace),
      new(1,2,1, TokenKind.OpenBracket),
      new(1,3,1, TokenKind.CloseBracket),
      new(1,4,0, TokenKind.EndOfLine)
    };

    var result = lexer.Tokenize(line, 1);

    result.Success.Should().BeTrue();
    result.IsEmpty.Should().BeFalse();
    result.Tokens.Should().BeEquivalentTo(expectedTokens);
  }

  [Fact]
  public void Tokenize_EmptyString_ReturnsExpectedToken()
  {
    var stringLexer = Substitute.For<IStringLexer>();
    var lexer = new LineLexer(stringLexer);

    var result = lexer.Tokenize(string.Empty, 1);

    result.Success.Should().BeTrue();
    result.IsEmpty.Should().BeTrue();
    result.Tokens.Should().BeEmpty();
  }

  [Theory]
  [InlineData("'", TokenKind.QuotedStringLiteral)]
  [InlineData("\"", TokenKind.DoubleQuotedStringLiteral)]
  [InlineData("'some string", TokenKind.QuotedStringLiteral)]
  [InlineData("\"some string", TokenKind.DoubleQuotedStringLiteral)]
  public void Tokenize_LineWithNonClosedString_ReturnsExpectedTokenWithError(string input, TokenKind tokenKind)
  {
    var lexer = new LineLexer(new StringLexer());

    var expectedTokens = new Token[]
    {
      new(1, 0, (ushort)input.Length, tokenKind, LexingError.StringLiteralIsNotClosed)
    };

    var result = lexer.Tokenize(input, 1, 0);

    result.Success.Should().BeFalse();
    result.IsEmpty.Should().BeFalse();
    result.Tokens.Should().BeEquivalentTo(expectedTokens);
  }
}