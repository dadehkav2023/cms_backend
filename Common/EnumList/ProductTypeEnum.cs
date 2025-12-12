using System.ComponentModel;

namespace Common.EnumList;

public enum ProductTypeEnum : short
{
    [Description("کالای فیزیکی")]
    PhysicalProduct = 1,
    [Description("خدمت")]
    Service = 2,
}