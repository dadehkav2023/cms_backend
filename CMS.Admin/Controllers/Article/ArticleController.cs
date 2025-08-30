using System.Threading.Tasks;
using Application.Services.Article;
using Application.Services.Article.Article;
using Application.ViewModels.Article.Attachment.Request;
using Application.ViewModels.Article.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.Article
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IArticleAttachmentService _articleAttachmentService;

        public ArticleController(IArticleService articleService,
            IArticleAttachmentService articleAttachmentService)
        {
            _articleService = articleService;
            _articleAttachmentService = articleAttachmentService;
        }

        //[Authorize]
        [HttpPost("NewArticle")]
        public async Task<IActionResult> NewArticle(
            [FromForm] RequestNewArticleViewModel requestNewArticleViewModel)
        {
            return (await _articleService.NewArticle(requestNewArticleViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPut("EditArticle")]
        public async Task<IActionResult> EditArticle(
            [FromForm] RequestEditArticleViewModel requestEditArticleViewModel)
        {
            return (await _articleService.EditArticle(requestEditArticleViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPost("GetArticle")]
        public async Task<IActionResult> GetArticle(
            [FromBody] RequestGetArticleViewModel requestGetArticleViewModel)
        {
            return (await _articleService.GetArticleList(requestGetArticleViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpDelete("DeleteArticle")]
        public async Task<IActionResult> DeleteArticle(
            [FromForm] int articleId)
        {
            return (await _articleService.DeleteArticle(articleId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPost("Attachment/NewAttachment")]
        public async Task<IActionResult> NewArticleAttachment(
            [FromForm] RequestNewArticleAttachmentViewModel requestNewArticleAttachmentViewModel)
        {

            return (await _articleAttachmentService.NewArticleAttachment(
                    requestNewArticleAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }
        
        //[Authorize]
        [HttpPut("Attachment/EditAttachment")]
        public async Task<IActionResult> EditArticleAttachment(
            [FromForm] RequestEditArticleAttachmentViewModel requestEditArticleAttachmentViewModel)
        {

            return (await _articleAttachmentService.EditArticleAttachment(
                    requestEditArticleAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }
        
        //[Authorize]
        [HttpPost("Attachment/GetAttachment")]
        public async Task<IActionResult> GetArticleAttachment(
            [FromForm] RequestGetArticleAttachmentViewModel requestGetArticleAttachmentViewModel)
        {

            return (await _articleAttachmentService.GetArticleAttachmentList(
                    requestGetArticleAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }
        
        //[Authorize]
        [HttpDelete("Attachment/DeleteAttachment")]
        public async Task<IActionResult> DeleteArticleAttachment(
            [FromForm] int articleAttachmentId)
        {

            return (await _articleAttachmentService.DeleteArticleAttachment(
                    articleAttachmentId))
                .ToWebApiResult()
                .ToHttpResponse();
        }
    }
}