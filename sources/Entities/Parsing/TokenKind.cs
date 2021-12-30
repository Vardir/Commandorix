﻿namespace Vardirsoft.Commandorix.Entities.Parsing;

public enum TokenKind : byte
{
  Unknown,
  /// <summary>
  /// Any char sequence that does not start with terminal symbol (f.e. name, 1.0, true)
  /// </summary>
  StringLiteral,
  /// <summary>
  /// Any char sequence enclosed in single quotes (f.e. 'name', '1.0', 'true')
  /// </summary>
  QuotedStringLiteral,
  /// <summary>
  /// Any char sequence enclosed in single quotes (f.e. "name", "1.0", "true")
  /// </summary>
  DoubleQuotedStringLiteral,
  OpenCurlyBracket,
  CloseCurlyBracket,
  OpenSquareBracket,
  CloseSquareBracket,
  OpenBracket,
  CloseBracket,
  DollarSign,
  Comma,
  Dash,
  Plus,
  Asterisk,
  Hash,
  Percent,
  Backslash,
  Colon,
  Semicolon,
  EndOfLine,
  EndOfFile,
  Whitespace,
  EqualSign
}

public enum LexingError
{
  None,
  ExpectedWhiteSpaceCharacter,
  StringLiteralIsNotClosed,
  Unknown,
}