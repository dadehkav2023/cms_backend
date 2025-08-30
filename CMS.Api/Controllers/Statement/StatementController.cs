using System.Threading.Tasks;
using Application.Services.Statement;
using Application.Services.Statement.Attachment;
using Application.Services.Statement.Category;
using Application.ViewModels.Statement.Attachment.Request;
using Application.ViewModels.Statement.Category.Request;
using Application.ViewModels.Statement.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.Statement
{
    [Route("api/[controller]")]
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


        #region Category

        //[Authorize]
        [HttpGet("Category/GetCategory")]
        public async Task<IActionResult> GetStatementCategory()
        {
            return (await _statementCategoryService.GetAllStatementCategoryList())
                .ToWebApiResult()
                .ToHttpResponse();
        }

        #endregion
    }
}