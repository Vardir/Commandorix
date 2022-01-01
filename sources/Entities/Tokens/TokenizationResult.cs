using System;

namespace Vardirsoft.Commandorix.Entities.Tokens;

public readonly ref struct TokenizationResult
{
  public readonly bool IsEmpty = true;
  public readonly bool Success;
  public readonly ReadOnlySpan<Token> Tokens;

  private TokenizationResult(bool success, bool isEmpty, in ReadOnlySpan<Token> tokens)
  {
    IsEmpty = isEmpty;
    Tokens = tokens;
    Success = success;
  }

  public static TokenizationResult Empty => new(true, true, ReadOnlySpan<Token>.Empty);

  public static TokenizationResult AsFinal(ReadOnlySpan<Token> tokens) => new(true, false, in tokens);

  public static TokenizationResult AsError(ushort index, ushort lastIndex, ReadOnlySpan<Token> tokens) => new(false, false, in tokens);
}