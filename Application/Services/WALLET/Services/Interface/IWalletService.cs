using System.Threading.Tasks;
using Application.BusinessLogic;
using Domain.Entities.Financial;

namespace Application.Services.WALLET.Services.Interface
{
    public interface IWalletService
    {
        long ChargeWallet(FinancialTransaction financialTransaction, bool save = false);
        Task<bool> SubtractPaymentFromWallet(long paymentAmount);
        
        Task<BusinessLogicResult<long>> GetWalletBalance();
    }
}