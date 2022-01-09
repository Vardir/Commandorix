namespace Vardirsoft.Commandorix.Entities.Parsing;

public enum ParsingError : byte
{
  None,
  Unknown,
  WrongDataType,
  UnexpectedTokenEncountered,
  NoCommandFound,
  InvalidVariableDefinition,
  InvalidVariableIdentifier,
  InvalidVariableAssignmentExpression,
  VariableNotFound,
  InvalidCommandDefinition,
  InvalidCommandIdentifier,
  CommandNotFound,
  ExpectedParameterIdentifier,
  InvalidParameterShortcut,
  InvalidParameterName,
  InvalidNumberFormat,
  InvalidDataType,
  UnknownOptionName,
}