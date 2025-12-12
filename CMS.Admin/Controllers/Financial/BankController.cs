using System.Threading.Tasks;
using Application.Services.WALLET.Services.Interface;
using Application.Services.WALLET.ViewModels;
using Application.ViewModels.ImageGallery.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.Financial;


[Route("api/[controller]")]
public class BankController(IBankService bankService) : Controller
{
    [HttpGet("[action]")]
    [Authorize]
    public async Task<IActionResult> GetSepToken(long amount)
    {
        return (await bankService.GetSepBankToken(amount)).ToWebApiResult()
            .ToHttpResponse();
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> VerifyPayment([FromForm] ShaparakVerifyPaymentRequestViewModel model)
    {
        var result = (await bankService.VerifyPayment(model)).Result;
        if (result.Success)
        {
            return Redirect(string.Format(result.ReturnUrl, result.RefNum, result.BasketCode));
        }

        return Redirect(string.Format(result.ReturnUrl, result.BasketCode));
    }
    
}