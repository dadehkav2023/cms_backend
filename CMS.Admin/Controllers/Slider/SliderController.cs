using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Services.Slider;
using Application.ViewModels.Slider.Request;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Admin.Controllers.Slider
{
    [Route("api/admin/[controller]")]
    [ApiController]

    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [Authorize(Roles = nameof(RoleEnum.Slider))]
        [HttpPost("CreateNewSlider")]
        public async Task<IActionResult> CreateNewSlider([FromForm] RequestCreateSliderViewModel request)
        {
            //todo Add SortOrder for Slider
            var userId = 1;
            
            return (await _sliderService.CreateNewSlider(request, userId)).ToWebApiResult().ToHttpResponse();
        } 
        
        [Authorize(Roles = nameof(RoleEnum.Slider))]
        [HttpPut("EditSlider")]
        public async Task<IActionResult> EditSlider([FromForm] RequestEditSliderViewModel requestEditSliderViewModel)
        {
            //todo Add SortOrder for Slider

            var userId = 1;
            //todo Get UserId
            return (await _sliderService.EditSlider(requestEditSliderViewModel, userId)).ToWebApiResult().ToHttpResponse();
        }
        
        [HttpPost("GetSlider")]
        public async Task<IActionResult> GetSlider([FromBody] RequestGetSliderListViewModel requestGetSliderListViewModel)
        {
            //todo Sort Based On SortOrder Field

            var userId = 1;
            //todo Get UserId
            return (await _sliderService.GetSlider(requestGetSliderListViewModel, userId)).ToWebApiResult().ToHttpResponse();
        }
        
        [Authorize(Roles = nameof(RoleEnum.Slider))]
        [HttpDelete("DeleteSlider")]
        public async Task<IActionResult> DeleteSlider([FromForm] int sliderId)
        {
            var userId = 1;
            //todo Get UserId
            return (await _sliderService.DeleteSlider(sliderId, userId)).ToWebApiResult().ToHttpResponse();
        }
        
    }
}
