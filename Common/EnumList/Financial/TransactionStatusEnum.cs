using System.ComponentModel;

namespace Common.EnumList.Financial;

public enum TransactionStatusEnum
{
    [Description("در انتظار")] Pending = 1,

    [Description("موفق")] Success = 2,
    [Description("ناموفق")] Failed = 3
}