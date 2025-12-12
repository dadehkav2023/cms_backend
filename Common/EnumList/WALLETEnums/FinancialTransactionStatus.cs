using System.ComponentModel;

namespace Common.EnumList.WALLETEnums;

public enum FinancialTransactionStatus
{
    [Description("موفق")]
    Succeeded = 1,

    [Description("ناموفق")]
    NotSucceeded = 2,

    [Description("در حال بررسی")]
    Pending = 3,
}