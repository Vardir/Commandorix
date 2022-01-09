namespace Vardirsoft.Commandorix.Entities.Metadata;

public sealed class VariableMetadata
{
  private static VariableMetadata? EmptySingleton;

  public VariableMetadata(string id, DataType dataType)
  {
    Id = id;
    DataType = dataType;
  }

  public string Id { get; }
  public DataType DataType { get; }

  public static VariableMetadata Empty => EmptySingleton ??= new VariableMetadata(string.Empty, DataType.Void);
}