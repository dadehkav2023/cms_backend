using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.Store;
using Application.ViewModels.Store.Order;
using Application.ViewModels.Store.Product;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.Store;

[Route("api/store/[controller]")]
[Authorize]
public class OrderController(IOrderService orderService) : Controller
{
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateNewOrder(
        [FromBody] RequestCreateNewOrderViewModel model, CancellationToken cancellationToken)
    {
        var userName = User.Claims.FirstOrDefault(x=>x.Type == "Username")!.Value;
        return (await orderService.CreateOrderAsync(model,userName, cancellationToken)).ToWebApiResult()
            .ToHttpResponse();
    }

}