using System;

using Vardirsoft.Commandorix.Entities.Metadata;
using Vardirsoft.Commandorix.Entities.Parsing;
using Vardirsoft.Commandorix.Entities.Tokens;

namespace Vardirsoft.Commandorix.DomainServices.Parsing;

public interface IParsingContext
{
  bool AllowEmptyCommand { get; }

  (bool found, VariableMetadata metadata) GetVariableMetadata(in ReadOnlySpan<char> identifier);
  (bool found, CommandMetadata metadata) GetCommandMetadata(in ReadOnlySpan<char> identifier);
  ReadOnlySpan<char> GetTokenValue(in Token token);
  ParsingResult ParseInnerExpression(in ReadOnlySpan<Token> tokens, ushort line, ushort tokenIndex);
}