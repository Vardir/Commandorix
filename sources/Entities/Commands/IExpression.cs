using Vardirsoft.Commandorix.Entities.Metadata;

namespace Vardirsoft.Commandorix.Entities.Commands;

public interface IExpression
{
  DataType ReturnDataType { get; }
}