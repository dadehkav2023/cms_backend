using System.Threading.Tasks;
using Application.Services.Rules;
using Application.Services.Rules.Attachment;
using Application.ViewModels.Rules.Attachment.Request;
using Application.ViewModels.Rules.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.Rules
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : Controller
    {
        private readonly IRulesService _rulesService;
        private readonly IRulesAttachmentService _rulesAttachmentService;

        public RulesController(IRulesService rulesService,
            IRulesAttachmentService rulesAttachmentService)
        {
            _rulesService = rulesService;
            _rulesAttachmentService = rulesAttachmentService;
        }

        
        //[Authorize]
        [HttpGet("GetRules")]
        public async Task<IActionResult> GetRules()
        {
            return (await _rulesService.GetRule())
                .ToWebApiResult()
                .ToHttpResponse();
        }

        
        //[Authorize]
        [HttpPost("Attachment/GetAttachment")]
        public async Task<IActionResult> GetRulesAttachment(
            [FromForm] RequestGetRulesAttachmentViewModel requestGetRulesAttachmentViewModel)
        {

            return (await _rulesAttachmentService.GetRulesAttachmentList(
                    requestGetRulesAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

    }
}