using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using Vardirsoft.Commandorix.Entities.Parsing;

namespace Vardirsoft.Commandorix.DomainServices.Parsing;

public sealed class LineLexer : ILexer
{
  private readonly IStringLexer _stringLexer;

  public LineLexer(IStringLexer stringLexer)
  {
    _stringLexer = stringLexer;
  }

  public TokenizationResult Tokenize(in ReadOnlySpan<char> input, ushort line, ushort position = 0)
  {
    if (input.IsEmpty)
      return TokenizationResult.Empty;

    var tokensList = new List<Token>(10);

    for (var index = position; index < input.Length;)
    {
      var token = input[index] switch
      {
        ' ' or '\t' => ConsumeWhitespace(in input, index, line),
        '-' => new(line, index, 1, TokenKind.Dash),
        '$' => new(line, index, 1, TokenKind.DollarSign),
        '(' => new(line, index, 1, TokenKind.OpenBracket),
        ')' => new(line, index, 1, TokenKind.CloseBracket),
        '\\' => new(line, index, 1, TokenKind.Backslash),
        ';' => new(line, index, 1, TokenKind.Semicolon),
        ',' => new(line, index, 1, TokenKind.Comma),
        ':' => new(line, index, 1, TokenKind.Colon),
        '=' => new(line, index, 1, TokenKind.EqualSign),
        '{' => new(line, index, 1, TokenKind.OpenCurlyBracket),
        '}' => new(line, index, 1, TokenKind.CloseCurlyBracket),
        '[' => new(line, index, 1, TokenKind.OpenSquareBracket),
        ']' => new(line, index, 1, TokenKind.CloseSquareBracket),
        '+' => new(line, index, 1, TokenKind.Plus),
        '*' => new(line, index, 1, TokenKind.Asterisk),
        '%' => new(line, index, 1, TokenKind.Percent),
        '#' => new(line, index, 1, TokenKind.Hash),
        '\n' => Token.EndOfLine(line, index),
        _ => _stringLexer.Tokenize(in input, line, index),
      };

      tokensList.Add(token);

      if (token.Error is not LexingError.None)
        return TokenizationResult.AsError(token.Position, (ushort)(token.Position + token.Length), tokensList.ToImmutableArray());

      if (token.Kind is TokenKind.EndOfLine)
        break;

      index = (ushort)(token.Position + token.Length);
    }

    if (tokensList.Count > 0 && tokensList[^1].Kind is not TokenKind.EndOfLine)
    {
      tokensList.Add(Token.EndOfLine(line, (ushort)input.Length));
    }

    return TokenizationResult.AsFinal(tokensList.ToImmutableArray());
  }

  private static Token ConsumeWhitespace(in ReadOnlySpan<char> input, ushort position, ushort line)
  {
    var index = position;

    for (; index < input.Length; index++)
    {
      if (input[index] is not ' ' and not '\t')
        break;
    }

    return new(line, position, (ushort)(index - position), TokenKind.Whitespace);
  }
}