using System.Threading.Tasks;
using Application.Services.AboutUs;
using Application.ViewModels.AboutUs.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.AboutUs
{
    [Route("api/[controller]")]
    [ApiController]

    public class AboutUsController : Controller
    {
        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        //[Authorize]
        [HttpGet("GetAboutUs")]
        public async Task<IActionResult> GetAboutUs()
        {

            return (await _aboutUsService.GetAboutUs()).ToWebApiResult().ToHttpResponse();
        }
        
    }
}
