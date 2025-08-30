using System.Threading.Tasks;
using Application.Services.ContactUs;
using Application.ViewModels.ContactUs.ContactUs.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.ContactUs
{
    [Route("api/admin/[controller]")]
    [ApiController]

    public class ContactUsController : Controller
    {
        private readonly IContactUsService _contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        //[Authorize]
        [HttpPost("SetContactUs")]
        public async Task<IActionResult> SetContactUs([FromForm] RequestSetContactUsViewModel requestSetContactUsViewModel)
        {
            return (await _contactUsService.SetContactUs(requestSetContactUsViewModel)).ToWebApiResult().ToHttpResponse();
        }

        //[Authorize]
        [HttpGet("GetContactUs")]
        public async Task<IActionResult> GetContactUs()
        {

            return (await _contactUsService.GetContactUs()).ToWebApiResult().ToHttpResponse();
        }
        
    }
}
