using System.Threading.Tasks;
using Application.Services.Article;
using Application.Services.Article.Article;
using Application.ViewModels.Article.Attachment.Request;
using Application.ViewModels.Article.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.Article
{
    [Route("api/[controller]")]
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
        [HttpPost("GetArticle")]
        public async Task<IActionResult> GetArticle(
            [FromBody] RequestGetArticleViewModel requestGetArticleViewModel)
        {
            return (await _articleService.GetArticleList(requestGetArticleViewModel))
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
    }
}