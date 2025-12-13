using System.Threading.Tasks;
using Application.Services.WALLET.Services.Interface;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.Financial;

[Route("api/[controller]")]
[Authorize]
public class UserFinancialController(IWalletService walletService) : Controller
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetCurrentUserWalletBalance()
    {
        return (await walletService.GetWalletBalance()).ToWebApiResult()
            .ToHttpResponse();
    }
}