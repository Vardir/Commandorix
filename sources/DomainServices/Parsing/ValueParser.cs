using System;
using System.Globalization;

namespace Vardirsoft.Commandorix.DomainServices.Parsing;

public sealed class ValueParser : IValueParser
{
  public (bool success, double number) TryParseNumber(in ReadOnlySpan<char> symbols, IFormatProvider formatProvider) => double.TryParse(symbols, NumberStyles.Float, formatProvider, out var number) ? (true, number) : (false, 0.0d);

  public (bool success, bool boolean) TryParseBoolean(in ReadOnlySpan<char> symbols)
  {
    if (symbols.IsEmpty)
      return (false, false);

    if (symbols.Length is not 1)
      return bool.TryParse(symbols, out var result) ? (true, result) : (false, false);

    var value = symbols[0];

    return (value is '0' or '1', value is '1');
  }
}