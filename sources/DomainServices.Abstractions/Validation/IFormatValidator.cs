using System;

namespace Vardirsoft.Commandorix.DomainServices.Validation;

public interface IFormatValidator
{
  bool ValidateVariableIdentifier(in ReadOnlySpan<char> symbols);
  bool ValidateCommandIdentifier(in ReadOnlySpan<char> symbols);
  bool ValidateCommandParameterShortcut(in ReadOnlySpan<char> symbols);
  bool ValidateCommandParameter(in ReadOnlySpan<char> symbols);
}