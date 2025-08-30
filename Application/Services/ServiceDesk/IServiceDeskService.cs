using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.ServiceDesk.Request;
using Application.ViewModels.ServiceDesk.Response;

namespace Application.Services.ServiceDesk
{
    public interface IServiceDeskService
    {
        Task<IBusinessLogicResult<bool>> NewServiceDesk(RequestNewServiceDeskViewModel requestNewServiceDeskViewModel, int userId);
        Task<IBusinessLogicResult<bool>> EditServiceDesk(RequestEditServiceDeskViewModel requestEditServiceDeskViewModel, int userId);
        Task<IBusinessLogicResult<ResponseGetServiceDeskListViewModel>> GetServiceDesk(RequestGetServiceDeskListViewModel requestGetServiceDeskListViewModel, int userId);
        Task<IBusinessLogicResult<bool>> RemoveServiceDesk(int serviceDeskId, int userId);
    }
}