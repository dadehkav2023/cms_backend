using System.ComponentModel;

namespace Common.EnumList;

public enum OrderStatusEnum : short
{
    [Description("در انتظار پرداخت")]
    WaitingForPayment = 0,
    [Description("پرداخت شده")]
    Payed = 1,
    [Description("تایید شده")]
    Accepted = 2,
    [Description("کنسل شده")]
    Cancelled = 3,
}