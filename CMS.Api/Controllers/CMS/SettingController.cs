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

namespace CMS.Api.Controllers.CMS
{
    [Route("api/[controller]")]
    [ApiController]

    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        //[Authorize]
        [HttpGet("GetSetting")]
        public async Task<IActionResult> GetSetting()
        {
            var userId = 1;
            //todo Get UserId
            return (await _settingService.GetSetting(userId)).ToWebApiResult().ToHttpResponse();
        }

        //todo Get Slider Action For Home Page Of CMS

    }
}
