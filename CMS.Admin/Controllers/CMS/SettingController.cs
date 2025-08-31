using Application.Services.CMS.Setting;
using Application.ViewModels.CMS.Setting.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Common.Enum;

namespace CMS.Admin.Controllers.CMS
{
    [Route("api/admin/[controller]")]
    [ApiController]

    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [Authorize(Roles = nameof(RoleEnum.CmsSetting))]
        [HttpPost("SetSetting")]
        public async Task<IActionResult> SetSetting([FromForm] RequestSetSettingViewModel requestSetSettingViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _settingService.SetSetting(requestSetSettingViewModel, userId)).ToWebApiResult().ToHttpResponse();
        }

        [HttpGet("GetSetting")]
        //[Authorize(Roles = "Operator")]
        public async Task<IActionResult> GetSetting()
        {
            var userId = 1;
            //todo Get UserId
            return (await _settingService.GetSetting(userId)).ToWebApiResult().ToHttpResponse();
        }

        //todo Get Slider Action For Home Page Of CMS

    }
}
