using System;

using Vardirsoft.Commandorix.Entities.Tokens;

namespace Vardirsoft.Commandorix.DomainServices.Helpers;

public static class TokenSequence
{
  public static (bool found, bool anyWhiteSpaceInBetween, ushort index) GetNextNonWhiteSpaceTokenPosition(in ReadOnlySpan<Token> tokens, ushort currentPosition, bool breakOnEndOfLine = true)
  {
    var anyWhiteSpaceFound = false;
    for (ushort index = (ushort)(currentPosition + 1); index < tokens.Length; index++)
    {
      var tokenKind = tokens[index].Kind;

      if (tokenKind is TokenKind.EndOfLine or TokenKind.EndOfFile)
      {
        if (breakOnEndOfLine)
          return (false, anyWhiteSpaceFound, 0);

        continue;
      }

      if (tokenKind is not TokenKind.Whitespace)
        return (true, anyWhiteSpaceFound, index);

      anyWhiteSpaceFound = true;
    }

    return (false, anyWhiteSpaceFound, 0);
  }

  public static (bool any, Token token) GetNextAfter(in ReadOnlySpan<Token> tokens, ushort position)
  {
    if (tokens.IsEmpty || position + 1 >= tokens.Length)
      return (false, new Token());

    return (true, tokens[position + 1]);
  }
}