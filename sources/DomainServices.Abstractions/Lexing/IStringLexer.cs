using System;

using Vardirsoft.Commandorix.Entities.Tokens;

namespace Vardirsoft.Commandorix.DomainServices.Lexing;

public interface IStringLexer
{
  Token Tokenize(in ReadOnlySpan<char> input, ushort line, ushort position);
}