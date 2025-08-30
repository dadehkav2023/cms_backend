using System.Threading.Tasks;
using Application.Services.AboutUs;
using Application.ViewModels.AboutUs.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.AboutUs
{
    [Route("api/admin/[controller]")]
    [ApiController]

    public class AboutUsController : Controller
    {
        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        //[Authorize]
        [HttpPost("SetAboutUs")]
        public async Task<IActionResult> SetAboutUs([FromForm] RequestSetAboutUsViewModel requestSetAboutUsViewModel)
        {
            return (await _aboutUsService.SetAboutUs(requestSetAboutUsViewModel)).ToWebApiResult().ToHttpResponse();
        }

        //[Authorize]
        [HttpGet("GetAboutUs")]
        public async Task<IActionResult> GetAboutUs()
        {

            return (await _aboutUsService.GetAboutUs()).ToWebApiResult().ToHttpResponse();
        }
        
    }
}
