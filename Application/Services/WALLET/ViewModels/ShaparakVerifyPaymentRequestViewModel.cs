namespace Application.Services.WALLET.ViewModels;

public class ShaparakVerifyPaymentRequestViewModel
{
    public string OrderId { get; set; }
    public string Token { get; set; }
    public string? HashedCardNo { get; set; }
    public string? PrimaryAccNo { get; set; }
    public string? SwitchResCode { get; set; }
    public string? CardHolderFullName { get; set; }
    public string ResCode { get; set; }
}


public class VerifyResultData
{
    public bool Succeed { get; set; }
    /// <summary>
    /// نتیجه تراکنش
    /// </summary>
    public string ResCode { get; set; }

    /// <summary>
    /// مبلغ تراکنش
    /// </summary>
    public long Amount { get; set; }

    /// <summary>
    /// شرح نتیجه تراکنش
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// شماره مرجع تراکنش
    /// </summary>
    public string RetrivalRefNo { get; set; }

    /// <summary>
    /// شماره پیگیری
    /// </summary>
    public string SystemTraceNo { get; set; }

    /// <summary>
    /// شماره سفارش
    /// </summary>
    public int? OrderId { get; set; }

    /// <summary>
    /// تاریخ تراکنش
    /// </summary>
    public string TransactionDate { get; set; }

    /// <summary>
    /// نام دارنده کارت
    /// </summary>
    public string CardHolderFullName { get; set; }
}

