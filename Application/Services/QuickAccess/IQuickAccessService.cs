using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.QuickAccess.Request;
using Application.ViewModels.QuickAccess.Response;

namespace Application.Services.QuickAccess
{
    public interface IQuickAccessService
    {
        Task<IBusinessLogicResult<bool>> NewQuickAccess(RequestNewQuickAccessViewModel requestNewQuickAccessViewModel);
        Task<IBusinessLogicResult<bool>> EditQuickAccess(RequestEditQuickAccessViewModel requestEditQuickAccessViewModel);
        Task<IBusinessLogicResult<ResponseGetQuickAccessListViewModel>> GetQuickAccessList(RequestGetQuickAccessViewModel requestGetQuickAccessViewModel);
        Task<IBusinessLogicResult<bool>> DeleteQuickAccess(int quickAccessId);
    }
}