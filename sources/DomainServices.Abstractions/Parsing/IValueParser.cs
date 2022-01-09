using System;

namespace Vardirsoft.Commandorix.DomainServices.Parsing;

public interface IValueParser
{
  (bool success, double number) TryParseNumber(in ReadOnlySpan<char> symbols, IFormatProvider formatProvider);
  (bool success, bool boolean) TryParseBoolean(in ReadOnlySpan<char> symbols);
}