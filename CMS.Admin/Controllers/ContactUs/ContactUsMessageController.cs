using System.Threading.Tasks;
using Application.Services.ContactUs;
using Application.Services.ContactUs.ContactUsMessage;
using Application.ViewModels.ContactUs.ContactUs.ContactUsMessage.Request;
using Application.ViewModels.ContactUs.ContactUs.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.ContactUs
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ContactUsMessageController : Controller
    {
        private readonly IContactUsMessageService _contactUsMessageService;

        public ContactUsMessageController(IContactUsMessageService contactUsMessageService)
        {
            _contactUsMessageService = contactUsMessageService;
        }

        //[Authorize]
        [HttpPost("GetContactUsMessage")]
        public async Task<IActionResult> GetContactUsMessage(
            [FromBody] RequestGetContactUsMessageViewModel requestGetContactUsMessageViewModel)
        {
            return (await _contactUsMessageService.GetContactUsMessageList(
                requestGetContactUsMessageViewModel)).ToWebApiResult().ToHttpResponse();
        }
    }
}