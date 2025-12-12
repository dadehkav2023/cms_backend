namespace Application.Services.WALLET.ViewModels;

public class ShaparakVerifyPaymentResponseViewModel
{
    public bool Success { get; set; }
    public string RefNum { get; set; }
    public string ResCode { get; set; }

    public string? BasketCode { get; set; }
    
    public string ReturnUrl { get; set; }
}