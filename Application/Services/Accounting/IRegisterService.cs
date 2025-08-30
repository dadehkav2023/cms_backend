using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Accounting;
using Application.ViewModels.Accounting.Request;

namespace Application.Services.Accounting
{
    public interface IRegisterService
    {
        Task<IBusinessLogicResult<bool>> SendValidationCode(string mobileNumber);
        Task<IBusinessLogicResult<bool>> CheckValidationCode(RequestCheckValidationCodeViewModel requestCheckValidationCode);
        Task<IBusinessLogicResult<bool>> Register(RequestRegisterViewModel requestRegisterViewModel);
    }
}