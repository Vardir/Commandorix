using Vardirsoft.Commandorix.Entities.Metadata;

namespace Vardirsoft.Commandorix.Entities.Commands;

public sealed class EmptyExpression : IExpression
{
  private static EmptyExpression? Singleton;

  private EmptyExpression() { }

  public DataType ReturnDataType => DataType.Void;

  public static EmptyExpression Instance => Singleton ??= new EmptyExpression();
}