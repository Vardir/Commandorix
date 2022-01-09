using System;

using Vardirsoft.Commandorix.Entities.Commands;
using Vardirsoft.Commandorix.Entities.Metadata;

namespace Vardirsoft.Commandorix.DomainServices.Factories;

public interface IExpressionFactory
{
  IExpression CreateVariableReadCommand(in ReadOnlySpan<char> identifier);
  IExpression CreateVariableAssignmentCommand(in ReadOnlySpan<char> identifier, IExpression nestedExpression);
  IExpression CreateCommandCall(in ReadOnlySpan<char> identifier);
  ICommandExpressionBuilder CreateCommandBuilder(CommandMetadata commandMetadata);
}