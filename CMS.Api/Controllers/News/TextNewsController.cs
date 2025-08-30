using System.Threading.Tasks;
using Application.Services.News;
using Application.Services.News.Attachment;
using Application.Services.News.Category;
using Application.Services.News.PhotoNews;
using Application.Services.News.TextNews;
using Application.Services.News.TextNews.Attachment;
using Application.ViewModels.News.Category.Request;
using Application.ViewModels.News.PhotoNews.Request;
using Application.ViewModels.News.Request;
using Application.ViewModels.News.TextNews.Attachment.Request;
using Application.ViewModels.News.TextNews.Request;
using CMS.Api.Helper.Response;
using Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.News
{
    [Route("api/News/[controller]")]
    [ApiController]
    public class TextNewsController : Controller
    {
        private readonly ITextNewsService _textNewsService;
        private readonly ITextNewsAttachmentService _textNewsAttachmentService;

        public TextNewsController(ITextNewsService textNewsService,
        ITextNewsAttachmentService textNewsAttachmentService)
        {
            _textNewsService = textNewsService;
            _textNewsAttachmentService = textNewsAttachmentService;
        }


        //[Authorize]
        [HttpPost("GetNews")]
        public async Task<IActionResult> GetNews(
            [FromBody] RequestGetTextNewsViewModel requestGetTextNewsViewModel)
        {
            return (await _textNewsService.GetNews(requestGetTextNewsViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }
       
        #region Attachment

      
        //[Authorize]
        [HttpPost("Attachment/GetAttachment")]
        public async Task<IActionResult> GetNewsAttachment(
            [FromForm] RequestTextGetNewsAttachmentViewModel requestTextGetNewsAttachmentViewModel)
        {
            return (await _textNewsAttachmentService.GetNewsAttachmentList(
                    requestTextGetNewsAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        #endregion
    }
}