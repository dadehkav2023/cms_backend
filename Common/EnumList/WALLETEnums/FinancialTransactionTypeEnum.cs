using System.ComponentModel;

namespace Common.EnumList.WALLETEnums;

public enum FinancialTransactionTypeEnum
{
    [Description("شارژ کیف پول")]
    ChargeWallet = 1,

    [Description("پرداخت از کیف پول")]
    PayFromWallet = 2,

    [Description("پرداخت از درگاه بانک")]
    PayFromBank = 3,

    [Description("عودت مالی")]
    FinancialReturn = 4,

}