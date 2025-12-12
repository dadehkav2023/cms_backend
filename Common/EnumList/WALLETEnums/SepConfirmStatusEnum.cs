using System.ComponentModel;

namespace Common.EnumList.WALLETEnums;

public enum SepConfirmStatusEnum
{
    [Description("موفق")]
    Succeeded = 1,

    [Description("ناموفق")]
    NotSucceeded = 2,
}