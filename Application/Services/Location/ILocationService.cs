using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Public;

namespace Application.Services.Location
{
    public interface ILocationService
    {
        Task<IBusinessLogicResult<List<SelectOptionViewModel>>> GetAllProvincesAsync(CancellationToken cancellationToken);
        Task<IBusinessLogicResult<List<SelectOptionViewModel>>> GetAllCountiesByProvinceIdAsync(int provinceId, CancellationToken cancellationToken);
        Task<IBusinessLogicResult<List<SelectOptionViewModel>>> GetAllRuralsByCountyIdAsync(int countyId, CancellationToken cancellationToken);
        Task<IBusinessLogicResult<List<SelectOptionViewModel>>> GetAllCitiesByCountyIdAsync(int countyId, CancellationToken cancellationToken);
        Task<IBusinessLogicResult<List<SelectOptionViewModel>>> GetAllVillagesByCountyIdAsync(int countyId, CancellationToken cancellationToken);
    }
}
