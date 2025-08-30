using System.Threading.Tasks;
using Application.Services.Rules;
using Application.Services.Rules.Attachment;
using Application.ViewModels.Rules.Attachment.Request;
using Application.ViewModels.Rules.Request;
using CMS.Admin.Helper.Response;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.Rules
{
    [Route("api/admin/[controller]")]
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

        [Authorize(Roles = nameof(RoleEnum.CmsSetting))]
        [HttpPost("SetRules")]
        public async Task<IActionResult> SetRule(
            [FromForm] RequestSetRulesViewModel requestSetRulesViewModel)
        {
            return (await _rulesService.SetRule(requestSetRulesViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpGet("GetRules")]
        public async Task<IActionResult> GetRules()
        {
            return (await _rulesService.GetRule())
                .ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.CmsSetting))]
        [HttpPost("Attachment/NewAttachment")]
        public async Task<IActionResult> NewRulesAttachment(
            [FromForm] RequestNewRulesAttachmentViewModel requestNewRulesAttachmentViewModel)
        {

            return (await _rulesAttachmentService.NewRulesAttachment(
                    requestNewRulesAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.CmsSetting))]
        [HttpPut("Attachment/EditAttachment")]
        public async Task<IActionResult> EditRulesAttachment(
            [FromForm] RequestEditRulesAttachmentViewModel requestEditRulesAttachmentViewModel)
        {

            return (await _rulesAttachmentService.EditRulesAttachment(
                    requestEditRulesAttachmentViewModel))
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

        [Authorize(Roles = nameof(RoleEnum.CmsSetting))]
        [HttpDelete("Attachment/DeleteAttachment")]
        public async Task<IActionResult> DeleteRulesAttachment(
            [FromForm] int rulesAttachmentId)
        {

            return (await _rulesAttachmentService.DeleteRulesAttachment(
                    rulesAttachmentId))
                .ToWebApiResult()
                .ToHttpResponse();
        }
    }
}