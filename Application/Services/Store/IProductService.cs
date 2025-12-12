using System.Threading;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Store.Product;

namespace Application.Services.Store;

public interface IProductService
{
    Task<BusinessLogicResult<int>> SetProductAsync(RequestSetProductViewModel model, CancellationToken ct);
    Task<BusinessLogicResult<int>> RemoveProductAsync(int id, CancellationToken ct);
    Task<BusinessLogicResult<ResponseGetAllProductViewModel>> GetAllProductsByFilterAsync(RequestGetAllProductByFilterViewModel model, CancellationToken ct);
    Task<BusinessLogicResult<ResponseGetAllProductViewModel>> GetAllProductsAsync(RequestGetAllProductByFilterViewModel model, CancellationToken ct);
    
}