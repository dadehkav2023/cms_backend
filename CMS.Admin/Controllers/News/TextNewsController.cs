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
using CMS.Admin.Helper.Response;
using Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.News
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
        [HttpPost("Attachment/NewAttachment")]
        public async Task<IActionResult> NewNewsAttachment(
            [FromForm] RequestTextNewNewsAttachmentViewModel requestTextNewNewsAttachmentViewModel)
        {
            return (await _textNewsAttachmentService.NewNewsAttachment(
                    requestTextNewNewsAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPut("Attachment/EditAttachment")]
        public async Task<IActionResult> EditNewsAttachment(
            [FromForm] RequestEditTextNewsAttachmentViewModel requestEditTextNewsAttachmentViewModel)
        {
            return (await _textNewsAttachmentService.EditNewsAttachment(
                    requestEditTextNewsAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

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

        //[Authorize]
        [HttpDelete("Attachment/DeleteAttachment")]
        public async Task<IActionResult> DeleteNewsAttachment(
            [FromForm] int newsAttachmentId)
        {
            return (await _textNewsAttachmentService.DeleteNewsAttachment(
                    newsAttachmentId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        #endregion
    }
}