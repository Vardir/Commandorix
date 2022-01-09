using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Vardirsoft.Commandorix.Entities.Commands;
using Vardirsoft.Commandorix.Entities.Tokens;

namespace Vardirsoft.Commandorix.Entities.Parsing;

public readonly struct ParsingResult
{
  private readonly IExpression? _expression;
  private readonly ParsingError _parsingError;
  private readonly LexingError _lexingError;
  private readonly ushort _line;
  private readonly ushort _location;

  private ParsingResult(IExpression? expression, ushort line, ushort location, LexingError lexingError, ParsingError parsingError)
  {
    _line = line;
    _location = location;
    _expression = expression;
    _lexingError = lexingError;
    _parsingError = parsingError;
  }

  public ParsingResult(IExpression expression) : this(expression, 0, 0, LexingError.None, ParsingError.None)
  {

  }

  public ParsingResult(ushort line, ushort location, ParsingError parsingError) : this(null, line, location, LexingError.None, parsingError)
  {
    if (parsingError is ParsingError.None)
      throw new InvalidOperationException();
  }

  public ParsingResult(ushort line, ushort location, LexingError lexingError) : this(null, line, location, lexingError, ParsingError.None)
  {
    if (lexingError is LexingError.None)
      throw new InvalidOperationException();
  }

  public ParsingResult(in Token token, ParsingError parsingError) : this(token.Line, token.Position, parsingError)
  {

  }

  public ParsingResult(in Token token, LexingError lexingError) : this(token.Line, token.Position, lexingError)
  {

  }

  public bool IsSuccess
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining), DebuggerStepThrough]
    get => _parsingError is ParsingError.None && _lexingError is LexingError.None;
  }

  public bool IsFailure
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining), DebuggerStepThrough]
    get => _parsingError is not ParsingError.None || _lexingError is not LexingError.None;
  }

  public ushort Line => _line;
  public ushort Location => _location;
  public ParsingError ParsingError => _parsingError;
  public LexingError LexingError => _lexingError;
  public IExpression Expression => IsSuccess ? _expression! : throw new InvalidOperationException();
}