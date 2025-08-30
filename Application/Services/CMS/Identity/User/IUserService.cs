using System.Collections.Generic;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Accounting;
using Application.ViewModels.Accounting.ForgetPass.ByEmail;
using Application.ViewModels.Accounting.ForgetPass.ByPhone;
using Application.ViewModels.Accounting.User.Request;
using Application.ViewModels.Accounting.User.Response;
using Application.ViewModels.CMS.Identity.User;

namespace Application.Services.CMS.Identity.User
{
    public interface IUserService
    {
        Task<IBusinessLogicResult<ResponseGetUserListViewModel>> GetUser(
            RequestGetUserListViewModel requestGetUserListViewModel);

        Task<IBusinessLogicResult<TokenViewModel>> LoginUser(LoginViewModel login);
        Task<IBusinessLogicResult<bool>> UserPotentialSaveAsync(UserPotentialInViewModel model);
        Task<IBusinessLogicResult<string>> UserPotentialPhoneConfirmAsync(UserPotentialPhoneConfirmViewModel model);
        Task<IBusinessLogicResult<int>> RegisterUserRealPotential(RegisterUserRealViewModel model);
        Task<IBusinessLogicResult<int>> RegisterUserLegalPotential(RegisterUserLegalViewModel model);
        Task<IBusinessLogicResult<int>> RegisterUserReal(RegisterUserRealViewModel model);
        Task<IBusinessLogicResult<int>> RegisterUserLegal(RegisterUserLegalViewModel model);
        Task<IBusinessLogicResult<int>> ForgotPasswordByPhoneAsync(PhoneConfirmViewmodel model);
        Task<IBusinessLogicResult<int>> ResetPasswordByPhoneNumber(ResetPasswordByPhonenumberViewModel model);
        Task<IBusinessLogicResult<int>> UserPhoneConfirmAsync(UserPhoneConfirmViewModel model);
        Task<IBusinessLogicResult<int>> ForgotPasswordByEmailAsync(ForgetPasswordByEmailViewmodel model);
        Task<IBusinessLogicResult<int>> ResetPasswordByEmail(ResetPasswordByEmailViewModel model);
        Task<IBusinessLogicResult<int>> UserEmailConfirmAsync(UserEmailConfirmViewModel model);
        Task<IBusinessLogicResult<int>> ChangePassword(ChangePassword model);
        Task<IBusinessLogicResult<bool>> LogUser(RequestLogUserViewModel model);
    }
}