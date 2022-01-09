using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Vardirsoft.Commandorix.DomainServices.Validation;

public sealed class FormatValidator : IFormatValidator
{
  [MethodImpl(MethodImplOptions.AggressiveInlining), DebuggerStepThrough]
  public bool ValidateVariableIdentifier(in ReadOnlySpan<char> symbols) => ValidateRegularIdentifier(symbols);

  [MethodImpl(MethodImplOptions.AggressiveInlining), DebuggerStepThrough]
  public bool ValidateCommandIdentifier(in ReadOnlySpan<char> symbols) => ValidateRegularIdentifier(symbols);

  public bool ValidateCommandParameterShortcut(in ReadOnlySpan<char> symbols)
  {
    return symbols.Length is 1 && char.IsLetter(symbols[0]);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining), DebuggerStepThrough]
  public bool ValidateCommandParameter(in ReadOnlySpan<char> symbols) => symbols.Length > 1 && ValidateRegularIdentifier(in symbols);

  private static bool ValidateRegularIdentifier(in ReadOnlySpan<char> symbols)
  {
    if (symbols.IsEmpty)
      return false;

    var symbol = symbols[0];

    if (char.IsLetter(symbol) is false && symbol is not '_')
      return false;

    if (symbols[^1] is '-')
      return false;

    for (var index = 1; index < symbols.Length; index++)
    {
      symbol = symbols[index];

      if (char.IsLetterOrDigit(symbol) is false && symbol is not '_' && symbol is not '-')
        return false;
    }

    return true;
  }
}