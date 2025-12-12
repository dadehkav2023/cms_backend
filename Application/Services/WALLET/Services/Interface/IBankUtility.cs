using System.Threading.Tasks;

namespace Application.Services.WALLET.Services.Interface;

public interface IBankUtility
{
    public Task<string> GetPaymentToken(long payAmount, string redirectUrl, string resNum, string additionalData1 = "", string additionalData2 = "");

    public Task<double> VerifyPayment(string refNum);

    public Task<double> ReversePayment(string refNum);
}