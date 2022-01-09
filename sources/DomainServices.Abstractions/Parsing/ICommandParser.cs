using System;

using Vardirsoft.Commandorix.Entities.Parsing;
using Vardirsoft.Commandorix.Entities.Tokens;

namespace Vardirsoft.Commandorix.DomainServices.Parsing;

public interface ICommandParser
{
  ParsingResult Parse(in ReadOnlySpan<Token> tokens, ushort line, ushort tokenIndex, IParsingContext context);
}