namespace Vardirsoft.Commandorix.Entities.Metadata;

public sealed class OptionMetadata
{
  private static OptionMetadata? Singleton;

  public OptionMetadata(string id, byte index, bool required, DataType dataType)
  {
    Id = id;
    Index = index;
    Required = required;
    DataType = dataType;
  }

  public string Id { get; }
  public byte Index { get; }
  public bool Required { get; }
  public DataType DataType { get; }
  public static OptionMetadata Empty => Singleton ??= new OptionMetadata(string.Empty, 0, false, DataType.Void);
}