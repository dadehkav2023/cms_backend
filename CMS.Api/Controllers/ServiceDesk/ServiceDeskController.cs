using System.Threading.Tasks;
using Application.Services.ServiceDesk;
using Application.Services.Slider;
using Application.ViewModels.ServiceDesk.Request;
using Application.ViewModels.Slider;
using Application.ViewModels.Slider.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.ServiceDesk
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceDeskController : Controller
    {
        private readonly IServiceDeskService _serviceDeskService;

        public ServiceDeskController(IServiceDeskService serviceDeskService)
        {
            _serviceDeskService = serviceDeskService;
        }


        //[Authorize]
        [HttpPost("GetServiceDesk")]
        public async Task<IActionResult> GetServiceDesk(
            [FromBody] RequestGetServiceDeskListViewModel requestGetServiceDeskListView)
        {
            var userId = 1;
            //todo Get UserId
            return (await _serviceDeskService.GetServiceDesk(requestGetServiceDeskListView, userId)).ToWebApiResult()
                .ToHttpResponse();
        }
    }
}