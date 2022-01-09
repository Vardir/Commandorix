using System;

using Vardirsoft.Commandorix.Utils.Enums;

namespace Vardirsoft.Commandorix.Utils.Helpers;

public static class CharSequence
{
  public static VerbalCount CountLeadingCharsVerbally(in ReadOnlySpan<char> input, char character, ushort position)
  {
    var count = CountLeadingChars(in input, character, position);

    if (count is 0)
      return VerbalCount.None;

    return count % 2 is 0 ? VerbalCount.Even : VerbalCount.Odd;
  }

  public static ushort CountLeadingChars(in ReadOnlySpan<char> input, char character, ushort position)
  {
    if (position - 1 <= 0)
      return 0;

    ushort level = 0;
    var index = position - 1;

    while (input[index] == character)
    {
      index--;
      level++;
    }

    return level;
  }
}