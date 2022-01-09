using System;

using Vardirsoft.Commandorix.Entities.Commands;
using Vardirsoft.Commandorix.Entities.Parsing;

namespace Vardirsoft.Commandorix.DomainServices.Parsing;

public interface ICommandLineParser
{
  ParsingResult Parse(in ReadOnlySpan<char> input, ushort line, IParsingContext context);
}