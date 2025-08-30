using System.Threading.Tasks;
using Application.Services.ServiceDesk;
using Application.Services.Slider;
using Application.ViewModels.ServiceDesk.Request;
using Application.ViewModels.Slider;
using Application.ViewModels.Slider.Request;
using CMS.Admin.Helper.Response;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.ServiceDesk
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ServiceDeskController : Controller
    {
        private readonly IServiceDeskService _serviceDeskService;

        public ServiceDeskController(IServiceDeskService serviceDeskService)
        {
            _serviceDeskService = serviceDeskService;
        }

        [Authorize(Roles = nameof(RoleEnum.ServiceDesk))]
        [HttpPost("CreateNewServiceDesk")]
        public async Task<IActionResult> CreateNewServiceDesk(
            [FromForm] RequestNewServiceDeskViewModel requestNewServiceDeskViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _serviceDeskService.NewServiceDesk(requestNewServiceDeskViewModel, userId)).ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.ServiceDesk))]
        [HttpPut("EditServiceDesk")]
        public async Task<IActionResult> EditServiceDesk(
            [FromForm] RequestEditServiceDeskViewModel requestEditServiceDeskViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _serviceDeskService.EditServiceDesk(requestEditServiceDeskViewModel, userId)).ToWebApiResult()
                .ToHttpResponse();
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
        
        [Authorize(Roles = nameof(RoleEnum.ServiceDesk))]
        [HttpDelete("DeleteServiceDesk")]
        public async Task<IActionResult> DeleteServiceDesk(
            [FromForm] int serviceDeskId)
        {
            var userId = 1;
            //todo Get UserId
            return (await _serviceDeskService.RemoveServiceDesk(serviceDeskId, userId)).ToWebApiResult()
                .ToHttpResponse();
        }
    }
}