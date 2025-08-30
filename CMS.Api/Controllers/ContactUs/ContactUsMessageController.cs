using System.Threading.Tasks;
using Application.Services.ContactUs;
using Application.Services.ContactUs.ContactUsMessage;
using Application.ViewModels.ContactUs.ContactUs.ContactUsMessage.Request;
using Application.ViewModels.ContactUs.ContactUs.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.ContactUs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsMessageController : Controller
    {
        private readonly IContactUsMessageService _contactUsMessageService;

        public ContactUsMessageController(IContactUsMessageService contactUsMessageService)
        {
            _contactUsMessageService = contactUsMessageService;
        }

        //[Authorize]
        [HttpPost("NewContactUsMessage")]
        public async Task<IActionResult> NewContactUsMessage(
            [FromBody] RequestNewContactUsMessageViewModel requestNewContactUsMessageViewModel)
        {
            return (await _contactUsMessageService.NewContactUsMessage(
                requestNewContactUsMessageViewModel )).ToWebApiResult().ToHttpResponse();
        }

    }
}