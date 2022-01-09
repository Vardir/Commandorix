using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vardirsoft.Commandorix.Utils.Helpers;

public static class EnumHelper
{
  public static IEnumerable<T> GetAllValues<T>() where T : Enum
  {
    var type = typeof(T);
    var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

    return fields.Select(f => f.GetValue(null)).OfType<T>();
  }
}