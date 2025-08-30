using System.Threading.Tasks;
using Application.Services.Accounting;
using Application.ViewModels.Accounting;
using Application.ViewModels.Accounting.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingController : Controller
    {
        private readonly IRegisterService _registerService;

        public AccountingController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost("SendValidationCode")]
        public async Task<IActionResult> SendValidationCode([FromForm] string mobileNumber)
        {
            return (await _registerService.SendValidationCode(mobileNumber)).ToWebApiResult().ToHttpResponse();
        }

        [HttpPost("CheckValidationCode")]
        public async Task<IActionResult> CheckValidationCode(
            [FromBody] RequestCheckValidationCodeViewModel requestCheckValidationCode)
        {
            return (await _registerService.CheckValidationCode(requestCheckValidationCode)).ToWebApiResult()
                .ToHttpResponse();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(
            [FromBody] RequestRegisterViewModel requestRegisterViewModel)
        {
            return (await _registerService.Register(requestRegisterViewModel)).ToWebApiResult()
                .ToHttpResponse();
        }
    }
}