using System.Collections.Generic;

namespace Vardirsoft.Commandorix.Entities.Metadata;

public abstract class CommandMetadata
{
  protected CommandMetadata(string id, DataType returnDataType, IReadOnlyDictionary<string, OptionMetadata> options)
  {
    Id = id;
    ReturnDataType = returnDataType;
    Options = options;
  }

  public string Id { get; }
  public DataType ReturnDataType { get; }
  public IReadOnlyDictionary<string, OptionMetadata> Options { get; }

  public virtual (bool found, OptionMetadata metadata) GetOptionMetadata(string optionName) => Options.TryGetValue(optionName, out var metadata) ? (true, metadata) : (false, OptionMetadata.Empty);
  public virtual byte GetOptionIndex(string parameterName) => Options[parameterName].Index;
  public virtual string GetOptionName(byte index)
  {
    foreach (var parameterMetadata in Options.Values)
    {
      if (parameterMetadata.Index == index)
        return parameterMetadata.Id;
    }

    return string.Empty;
  }
}