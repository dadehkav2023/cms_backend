using System.ComponentModel;

namespace Common.EnumList.Financial;

public enum TransactionLogStateEnum
{
    [Description("درخواست")] Request = 1,

    [Description("خروجی")] Response = 2,
}