using System;

using Vardirsoft.Commandorix.Entities.Tokens;
using Vardirsoft.Commandorix.Utils.Helpers;

namespace Vardirsoft.Commandorix.DomainServices.Lexing;

public sealed class StringLexer : IStringLexer
{
  public Token Tokenize(in ReadOnlySpan<char> input, ushort line, ushort position) => input[position] switch
  {
    '\'' => TokenizeQuotedString(in input, line, (ushort)(position + 1), '\''),
    '"' => TokenizeQuotedString(in input, line, (ushort)(position + 1), '"'),
    _ => TokenizeAnyStringUntilWhiteSpace(in input, line, position)
  };

  private static Token TokenizeQuotedString(in ReadOnlySpan<char> input, ushort line, ushort position, char terminalSymbol)
  {
    var index = position;
    var closed = false;

    for (; index < input.Length; index++)
    {
      var currentChar = input[index];

      if (input[index] is '\n' || currentChar == terminalSymbol && CharSequence.CountLeadingCharsVerbally(in input, '\\', index) is VerbalCount.None or VerbalCount.Even)
      {
        closed = currentChar == terminalSymbol;

        break;
      }
    }

    var error = closed is false ? LexingError.StringLiteralIsNotClosed : LexingError.None;
    var tokenKind = terminalSymbol is '\'' ? TokenKind.QuotedStringLiteral : TokenKind.DoubleQuotedStringLiteral;
    var lengthModifier = closed ? 2 : 1;

    return new(line, (ushort)(position - 1), (ushort)(index - position + lengthModifier), tokenKind, error);
  }

  private static Token TokenizeAnyStringUntilWhiteSpace(in ReadOnlySpan<char> input, ushort line, ushort position)
  {
    var index = position;

    for (; index < input.Length; index++)
    {
      if (input[index] is ' ' or '\t' or '\n')
        break;
    }

    return new(line, position, (ushort)(index - position), TokenKind.StringLiteral);
  }
}