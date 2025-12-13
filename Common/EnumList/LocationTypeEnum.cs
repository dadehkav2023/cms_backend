using System.ComponentModel;

namespace Common.EnumList
{
    public enum LocationTypeEnum
    {
        [Description("روستا")]
        Village = 1,

        [Description("شهر")]
        City = 2,

        [Description("دهستان")]
        RuralDistrict = 3,
    }
}
