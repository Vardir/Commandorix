using System.Linq;

using FluentAssertions;

using Vardirsoft.Commandorix.Utils.Helpers;

using Xunit;

namespace Utils.Tests.Helpers;

public class EnumHelperTests
{
  [Fact]
  public void GetAllValues_OnEnum_ReturnsAllDeclaredEnumValues()
  {
    var values = EnumHelper.GetAllValues<FixedEnum>().ToArray();

    values.Should().HaveCount(3);
    values.Should().ContainSingle(e => e == FixedEnum.Value1);
    values.Should().ContainSingle(e => e == FixedEnum.Value2);
    values.Should().ContainSingle(e => e == FixedEnum.Value3);
  }
}

public enum FixedEnum
{
  Value1,
  Value2,
  Value3
}