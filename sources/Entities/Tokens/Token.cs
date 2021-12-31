using System.Diagnostics;

namespace Vardirsoft.Commandorix.Entities.Tokens;

[DebuggerDisplay("{Kind} (line:{Line},pos:{Position},len:{Length}) err:{Error}")]
public readonly struct Token
{
  public readonly ushort Line;
  public readonly ushort Position;
  public readonly ushort Length;
  public readonly TokenKind Kind;
  public readonly LexingError Error;

  public Token(ushort line, ushort position, ushort length, TokenKind kind, LexingError error = LexingError.None)
  {
    Line = line;
    Position = position;
    Length = length;
    Kind = kind;
    Error = error;
  }

  public static Token EndOfLine(ushort line, ushort position) => new(line, position, 0, TokenKind.EndOfLine);
  public static Token EndOfFile(ushort line, ushort position) => new(line, position, 0, TokenKind.EndOfFile);
}