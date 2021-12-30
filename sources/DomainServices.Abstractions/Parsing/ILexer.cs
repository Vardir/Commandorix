using System;
using System.Collections.Immutable;

using Vardirsoft.Commandorix.Entities.Parsing;

namespace Vardirsoft.Commandorix.DomainServices.Parsing;

public interface ILexer
{
  TokenizationResult Tokenize(in ReadOnlySpan<char> input, ushort line, ushort position = 0);
}

public interface ILiteralLexer
{
  Token Tokenize(in ReadOnlySpan<char> input, ushort line, ushort position);
}

public interface IStringLexer
{
  Token Tokenize(in ReadOnlySpan<char> input, ushort line, ushort position);
}

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