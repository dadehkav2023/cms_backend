using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Rules.Request;
using Application.ViewModels.Rules.Response;

namespace Application.Services.Rules
{
    public interface IRulesService
    {
        Task<IBusinessLogicResult<bool>> SetRule(
            RequestSetRulesViewModel requestSetRulesViewModel);
        
        
        Task<IBusinessLogicResult<ResponseGetRulesViewModel>> GetRule();
       
    }
}