using System.Threading;
using System.Threading.Tasks;
using Application.Services.Store;
using Application.ViewModels.Store.Product;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.Store;

[Route("api/store/[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> GetAllProducts(
        [FromBody] RequestGetAllProductByFilterViewModel model, CancellationToken cancellationToken)
    {
        return (await _productService.GetAllProductsAsync(model, cancellationToken)).ToWebApiResult()
            .ToHttpResponse();
    }
    
}