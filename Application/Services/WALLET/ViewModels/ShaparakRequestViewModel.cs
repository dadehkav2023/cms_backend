using System;

namespace Application.Services.WALLET.ViewModels;

public class ShaparakRequestViewModel
{
    public string MerchantId { get; set; }

    public string TerminalId { get; set; }

    public long Amount { get; set; }

    public long OrderId { get; set; }

    public DateTime LocalDateTime { get; set; }

    public string ReturnUrl { get; set; }

    public string SignData { get; set; }


    //شماره تلفن کاربر
    public long UserId { get; set; }

    public string? ApplicationName { get; set; }
}