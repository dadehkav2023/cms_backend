using System.Threading.Tasks;
using Application.Services.QuickAccess;
using Application.ViewModels.QuickAccess.Request;
using Application.ViewModels.ServiceDesk.Request;
using CMS.Admin.Helper.Response;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.QuickAccess
{
    [Microsoft.AspNetCore.Components.Route("api/admin/[controller]")]
    [ApiController]
    public class QuickAccessController
    {
        private readonly IQuickAccessService _quickAccessService;

        public QuickAccessController(IQuickAccessService quickAccessService)
        {
            _quickAccessService = quickAccessService;
        }

        [Authorize(Roles = nameof(RoleEnum.QuickAccess))]
        [HttpPost("NewQuickAccess")]
        public async Task<IActionResult> NewQuickAccess(
            [FromBody] RequestNewQuickAccessViewModel requestNewQuickAccessViewModel)
        {
            return (await _quickAccessService.NewQuickAccess(requestNewQuickAccessViewModel)).ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.QuickAccess))]
        [HttpPut("EditQuickAccess")]
        public async Task<IActionResult> EditQuickAccess(
            [FromBody] RequestEditQuickAccessViewModel requestEditQuickAccessViewModel)
        {
            return (await _quickAccessService.EditQuickAccess(requestEditQuickAccessViewModel)).ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPost("GetQuickAccess")]
        public async Task<IActionResult> GetQuickAccess(
            [FromBody] RequestGetQuickAccessViewModel requestGetQuickAccessViewModel)
        {
            return (await _quickAccessService.GetQuickAccessList(requestGetQuickAccessViewModel)).ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.QuickAccess))]
        [HttpDelete("DeleteQuickAccess")]
        public async Task<IActionResult> DeleteQuickAccess(
            [FromForm] int quickAccessId)
        {
            return (await _quickAccessService.DeleteQuickAccess(quickAccessId)).ToWebApiResult()
                .ToHttpResponse();
        }
    }
}