using Application.BusinessLogic;
using Application.Services.CMS.Setting;
using Application.ViewModels.CMS.Setting.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Slider;
using Application.ViewModels.Slider;
using Application.ViewModels.Slider.Request;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace CMS.Api.Controllers.Slider
{
    [Route("api/[controller]")]
    [ApiController]

    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        //[Authorize]
        [HttpPost("GetSlider")]
        public async Task<IActionResult> GetSlider([FromBody] RequestGetSliderListViewModel requestGetSliderListViewModel)
        {
            //todo Sort Based On SortOrder Field

            var userId = 1;
            //todo Get UserId
            return (await _sliderService.GetSlider(requestGetSliderListViewModel, userId)).ToWebApiResult().ToHttpResponse();
        }
        
    }
}
