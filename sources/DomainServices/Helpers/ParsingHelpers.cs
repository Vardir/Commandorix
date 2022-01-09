using Vardirsoft.Commandorix.Entities.Commands;
using Vardirsoft.Commandorix.Entities.Parsing;

namespace Vardirsoft.Commandorix.DomainServices.Helpers;

public static class ParsingHelpers
{
  public static ParsingResult GetEmptyCommandOrError(bool allowEmptyCommands, ushort line, ushort position)
  {
    return allowEmptyCommands ? new ParsingResult(EmptyExpression.Instance) : new ParsingResult(line, position, ParsingError.NoCommandFound);
  }
}