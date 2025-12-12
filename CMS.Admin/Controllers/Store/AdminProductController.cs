using System.Threading;
using System.Threading.Tasks;
using Application.Services.Store;
using Application.ViewModels.Rules.Attachment.Request;
using Application.ViewModels.Store.Product;
using CMS.Admin.Helper.Response;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.Store;

[Authorize(Roles = "Admin")]
[Route("api/admin/[controller]")]

public class AdminProductController : Controller
{
    private readonly IProductService _productService;

    public AdminProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SetProduct(
        [FromForm] RequestSetProductViewModel model, CancellationToken cancellationToken)
    {
        return (await _productService.SetProductAsync(model, cancellationToken)).ToWebApiResult().ToHttpResponse();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GetAllProductsByFilter(
        [FromBody] RequestGetAllProductByFilterViewModel model, CancellationToken cancellationToken)
    {
        return (await _productService.GetAllProductsByFilterAsync(model, cancellationToken)).ToWebApiResult()
            .ToHttpResponse();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> RemoveProduct(
        int id, CancellationToken cancellationToken)
    {
        return (await _productService.RemoveProductAsync(id, cancellationToken)).ToWebApiResult()
            .ToHttpResponse();
    }
}