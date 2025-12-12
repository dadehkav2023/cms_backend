using System.Threading;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Store.Order;
using Domain.Entities.Store;

namespace Application.Services.Store;

public interface IOrderService
{
    Task<BusinessLogicResult<string>> CreateOrderAsync(RequestCreateNewOrderViewModel model, string userName,
        CancellationToken ct);
}