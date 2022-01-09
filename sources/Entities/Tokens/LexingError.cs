namespace Vardirsoft.Commandorix.Entities.Tokens;

public enum LexingError : byte
{
  None,
  Unknown,
  ExpectedWhiteSpaceCharacter,
  StringLiteralIsNotClosed,
}