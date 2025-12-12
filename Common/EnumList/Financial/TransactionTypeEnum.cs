using System.ComponentModel;

namespace Common.Enums.Financial;

public enum TransactionTypeEnum
{
    [Description("شارژ")] 
    Deposit = 1,

    [Description("برداشت")]
    Withdraw = 2
}