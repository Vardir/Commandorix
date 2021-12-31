using System.Collections.Immutable;

namespace Vardirsoft.Commandorix.Entities.Tokens;

public readonly ref struct TokenizationResult
{
  public readonly bool IsEmpty = true;
  public readonly bool Success;
  public readonly ImmutableArray<Token> Tokens;

  private TokenizationResult(bool success, bool isEmpty, in ImmutableArray<Token> tokens)
  {
    IsEmpty = isEmpty;
    Tokens = tokens;
    Success = success;
  }

  public static TokenizationResult Empty => new(true, true, in ImmutableArray<Token>.Empty);

  public static TokenizationResult AsFinal(ImmutableArray<Token> tokens) => new(true, false, in tokens);

  public static TokenizationResult AsError(ushort index, ushort lastIndex, ImmutableArray<Token> tokens) => new(false, false, in tokens);
}