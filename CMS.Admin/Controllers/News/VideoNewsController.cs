using System.Threading.Tasks;
using Application.Services.News.VideoNews;
using Application.Services.News.VideoNews;
using Application.Services.News.VideoNews.Attachment;
using Application.ViewModels.News.VideoNews.Request;
using Application.ViewModels.News.VideoNews.Attachment.Request;
using Application.ViewModels.News.VideoNews.Request;
using CMS.Admin.Helper.Response;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.News
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

        [Authorize(Roles = nameof(RoleEnum.VideoNews))]
        [HttpPost("Attachment/NewAttachment")]
        public async Task<IActionResult> NewNewsAttachment(
            [FromForm] RequestNewVideoNewsAttachmentViewModel requestVideoNewNewsAttachmentViewModel)
        {
            return (await _photoNewsAttachmentService.NewNewsAttachment(
                    requestVideoNewNewsAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        [Authorize(Roles = nameof(RoleEnum.VideoNews))]
        [HttpPut("Attachment/EditAttachment")]
        public async Task<IActionResult> EditNewsAttachment(
            [FromForm] RequestEditVideoNewsAttachmentViewModel requestEditVideoNewsAttachmentViewModel)
        {
            return (await _photoNewsAttachmentService.EditNewsAttachment(
                    requestEditVideoNewsAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

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

        //[Authorize]
        [HttpDelete("Attachment/DeleteAttachment")]
        public async Task<IActionResult> DeleteNewsAttachment(
            [FromForm] int newsAttachmentId)
        {
            return (await _photoNewsAttachmentService.DeleteNewsAttachment(
                    newsAttachmentId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        #endregion
    }
}