using System.Threading.Tasks;
using Application.Services.Statement;
using Application.Services.Statement.Attachment;
using Application.Services.Statement.Category;
using Application.ViewModels.Statement.Attachment.Request;
using Application.ViewModels.Statement.Category.Request;
using Application.ViewModels.Statement.Request;
using CMS.Admin.Helper.Response;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.Statement
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class StatementController : Controller
    {
        private readonly IStatementService _statementService;
        private readonly IStatementAttachmentService _statementAttachmentService;
        private readonly IStatementCategoryService _statementCategoryService;

        public StatementController(IStatementService statementService,
            IStatementAttachmentService statementAttachmentService, IStatementCategoryService statementCategoryService)
        {
            _statementService = statementService;
            _statementAttachmentService = statementAttachmentService;
            _statementCategoryService = statementCategoryService;
        }

        [Authorize(Roles = nameof(RoleEnum.Statement))]
        [HttpPost("NewStatement")]
        public async Task<IActionResult> NewStatement(
            [FromForm] RequestNewStatementViewModel requestNewStatementViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _statementService.NewStatement(requestNewStatementViewModel, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.Statement))]
        [HttpPut("EditStatement")]
        public async Task<IActionResult> EditStatement(
            [FromForm] RequestEditStatementViewModel requestEditStatementViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _statementService.EditStatement(requestEditStatementViewModel, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPost("GetStatement")]
        public async Task<IActionResult> GetStatement(
            [FromBody] RequestGetStatementViewModel requestGetStatementViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _statementService.GetStatementList(requestGetStatementViewModel, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.Statement))]
        [HttpDelete("DeleteStatement")]
        public async Task<IActionResult> DeleteStatement(
            [FromForm] int statementId)
        {
            var userId = 1;
            //todo Get UserId
            return (await _statementService.DeleteStatement(statementId, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.Statement))]
        [HttpPost("Attachment/NewAttachment")]
        public async Task<IActionResult> NewStatementAttachment(
            [FromForm] RequestNewStatementAttachmentViewModel requestNewStatementAttachmentViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _statementAttachmentService.NewStatementAttachment(
                    requestNewStatementAttachmentViewModel, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.Statement))]
        [HttpPut("Attachment/EditAttachment")]
        public async Task<IActionResult> EditStatementAttachment(
            [FromForm] RequestEditStatementAttachmentViewModel requestEditStatementAttachmentViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _statementAttachmentService.EditStatementAttachment(
                    requestEditStatementAttachmentViewModel, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPost("Attachment/GetAttachment")]
        public async Task<IActionResult> GetStatementAttachment(
            [FromForm] RequestGetStatementAttachmentViewModel requestGetStatementAttachmentViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _statementAttachmentService.GetStatementAttachmentList(
                    requestGetStatementAttachmentViewModel, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.Statement))]
        [HttpDelete("Attachment/DeleteAttachment")]
        public async Task<IActionResult> DeleteStatementAttachment(
            [FromForm] int statementAttachmentId)
        {
            var userId = 1;
            //todo Get UserId
            return (await _statementAttachmentService.DeleteStatementAttachment(
                    statementAttachmentId, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }


        #region Category

        [Authorize(Roles = nameof(RoleEnum.Statement))]
        [HttpPost("Category/NewCategory")]
        public async Task<IActionResult> NewStatementCategory(
            [FromForm] RequestNewStatementCategoryViewModel requestNewStatementCategoryViewModel)
        {
            return (await _statementCategoryService.NewStatementCategory(
                    requestNewStatementCategoryViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.Statement))]
        [HttpPut("Category/EditCategory")]
        public async Task<IActionResult> EditStatementCategory(
            [FromForm] RequestEditStatementCategoryViewModel requestEditStatementCategoryViewModel)
        {
            return (await _statementCategoryService.EditStatementCategory(
                    requestEditStatementCategoryViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpGet("Category/GetCategory")]
        public async Task<IActionResult> GetStatementCategory()
        {
            return (await _statementCategoryService.GetAllStatementCategoryList())
                .ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.Statement))]
        [HttpDelete("Category/DeleteCategory")]
        public async Task<IActionResult> DeleteStatementCategory(
            [FromForm] int statementCategoryId)
        {
            return (await _statementCategoryService.DeleteStatementCategory(
                    statementCategoryId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        #endregion
    }
}