using System;

using Vardirsoft.Commandorix.Entities.Tokens;

namespace Vardirsoft.Commandorix.DomainServices.Lexing;

public interface ILexer
{
  TokenizationResult Tokenize(in ReadOnlySpan<char> input, ushort line, ushort position = 0);
}