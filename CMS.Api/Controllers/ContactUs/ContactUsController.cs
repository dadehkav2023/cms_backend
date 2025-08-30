using System.Threading.Tasks;
using Application.Services.ContactUs;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.ContactUs
{
    [Route("api/[controller]")]
    [ApiController]

    public class ContactUsController : Controller
    {
        private readonly IContactUsService _contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        //[Authorize]
        [HttpGet("GetContactUs")]
        public async Task<IActionResult> GetContactUs()
        {

            return (await _contactUsService.GetContactUs()).ToWebApiResult().ToHttpResponse();
        }
        
    }
}
