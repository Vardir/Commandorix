using System;

using Vardirsoft.Commandorix.Entities.Commands;

namespace Vardirsoft.Commandorix.DomainServices.Factories;

public interface ICommandExpressionBuilder
{
  OptionSetResult TrySetOption(in ReadOnlySpan<char> identifier, bool value);
  OptionSetResult TrySetOption(in ReadOnlySpan<char> identifier, int value);
  OptionSetResult TrySetOption(in ReadOnlySpan<char> identifier, double value);
  OptionSetResult TrySetOption(in ReadOnlySpan<char> identifier, in ReadOnlySpan<char> value);
  OptionSetResult TryAutoSetOption(in ReadOnlySpan<char> identifier, in ReadOnlySpan<char> value);
  OptionSetResult TrySetOption(in ReadOnlySpan<char> identifier, IExpression command);
  OptionSetResult TrySetOption(int optionIndex, bool value);
  OptionSetResult TrySetOption(int optionIndex, int value);
  OptionSetResult TrySetOption(int optionIndex, double value);
  OptionSetResult TrySetOption(int optionIndex, in ReadOnlySpan<char> value);
  OptionSetResult TryAutoSetOption(int optionIndex, in ReadOnlySpan<char> value);
  OptionSetResult TrySetOption(int optionIndex, IExpression command);

  IExpression Build();
}

public enum OptionSetResult
{
  Success,
  UnknownOptionName,
  InvalidDataType
}