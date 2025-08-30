using System.Collections.Generic;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Map.Request;
using Application.ViewModels.Map.Response;

namespace Application.Services.Map
{
    public interface IMapService
    {
        Task<IBusinessLogicResult<bool>> SetProvinceMap(
            RequestSetProvinceMapViewModel requestSetProvinceMapViewModel);
        
        Task<IBusinessLogicResult<List<ResponseGetProvinceViewModel>>> GetProvinceList();
        
        Task<IBusinessLogicResult<List<ResponseGetProvinceMapViewModel>>> GetProvinceMap();
    }
}