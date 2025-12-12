using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.Services.WALLET.ViewModels;

namespace Application.Services.WALLET.Services.Interface;

public interface IBankService
{
    Task<BusinessLogicResult<GetSepBankTokenViewModel>> GetSepBankToken(long amount);

    Task<BusinessLogicResult<ShaparakVerifyPaymentResponseViewModel>> VerifyPayment(ShaparakVerifyPaymentRequestViewModel model);
}
