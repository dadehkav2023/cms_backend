using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Store.Order;

namespace Application.Services.Store;

public interface IOrderService
{
    Task<BusinessLogicResult<string>> CreateOrderAsync(RequestCreateNewOrderViewModel model, string userName, CancellationToken ct);
    Task<BusinessLogicResult<List<ResponseGetCurrentUserOrdersViewModel>>> GetCurrentUserOrdersAsync(int userId, CancellationToken ct);
}
