using System.ComponentModel;

namespace Common.EnumList.WALLETEnums;

public enum PaymentTypeEnum
{
    [Description("درگاه اینترنتی")]
    Online =1,
    [Description("شعبات بانک کشاورزی")]
    BankBranch = 2
}