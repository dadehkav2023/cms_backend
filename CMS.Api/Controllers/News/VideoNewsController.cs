using System.Threading.Tasks;
using Application.Services.News.VideoNews;
using Application.Services.News.VideoNews;
using Application.Services.News.VideoNews.Attachment;
using Application.ViewModels.News.VideoNews.Request;
using Application.ViewModels.News.VideoNews.Attachment.Request;
using Application.ViewModels.News.VideoNews.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.News
{
    [Route("api/News/[controller]")]
    [ApiController]
    public class VideoNewsController : Controller
    {
        private readonly IVideoNewsService _photoNewsService;
        private readonly IVideoNewsAttachmentService _photoNewsAttachmentService;

        public VideoNewsController(IVideoNewsService photoNewsService,
        IVideoNewsAttachmentService photoNewsAttachmentService)
        {
            _photoNewsService = photoNewsService;
            _photoNewsAttachmentService = photoNewsAttachmentService;
        }


        //[Authorize]
        [HttpPost("GetNews")]
        public async Task<IActionResult> GetNews(
            [FromBody] RequestGetVideoNewsViewModel requestGetVideoNewsViewModel)
        {
            return (await _photoNewsService.GetVideoNews(requestGetVideoNewsViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }
       
        #region Attachment

        //[Authorize]
        [HttpPost("Attachment/GetAttachment")]
        public async Task<IActionResult> GetNewsAttachment(
            [FromForm] RequestGetVideoNewsAttachmentViewModel requestVideoGetNewsAttachmentViewModel)
        {
            return (await _photoNewsAttachmentService.GetNewsAttachmentList(
                    requestVideoGetNewsAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        #endregion
    }
}