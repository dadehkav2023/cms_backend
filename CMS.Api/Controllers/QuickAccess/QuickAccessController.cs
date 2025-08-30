using System.Threading.Tasks;
using Application.Services.QuickAccess;
using Application.ViewModels.QuickAccess.Request;
using Application.ViewModels.ServiceDesk.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.QuickAccess
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class QuickAccessController
    {
        private readonly IQuickAccessService _quickAccessService;

        public QuickAccessController(IQuickAccessService quickAccessService)
        {
            _quickAccessService = quickAccessService;
        }
        //[Authorize]
        [HttpPost("GetQuickAccess")]
        public async Task<IActionResult> GetQuickAccess(
            [FromBody] RequestGetQuickAccessViewModel requestGetQuickAccessViewModel)
        {
            return (await _quickAccessService.GetQuickAccessList(requestGetQuickAccessViewModel)).ToWebApiResult()
                .ToHttpResponse();
        }
    }
}