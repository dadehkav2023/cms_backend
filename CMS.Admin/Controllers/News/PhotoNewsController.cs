using System.Threading.Tasks;
using Application.Services.News.PhotoNews;
using Application.Services.News.PhotoNews;
using Application.Services.News.PhotoNews.Attachment;
using Application.ViewModels.News.PhotoNews.Request;
using Application.ViewModels.News.PhotoNews.Attachment.Request;
using Application.ViewModels.News.PhotoNews.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.News
{
    [Route("api/News/[controller]")]
    [ApiController]
    public class PhotoNewsController : Controller
    {
        private readonly IPhotoNewsService _photoNewsService;
        private readonly IPhotoNewsAttachmentService _photoNewsAttachmentService;

        public PhotoNewsController(IPhotoNewsService photoNewsService,
        IPhotoNewsAttachmentService photoNewsAttachmentService)
        {
            _photoNewsService = photoNewsService;
            _photoNewsAttachmentService = photoNewsAttachmentService;
        }


        //[Authorize]
        [HttpPost("GetNews")]
        public async Task<IActionResult> GetNews(
            [FromBody] RequestGetPhotoNewsViewModel requestGetPhotoNewsViewModel)
        {
            return (await _photoNewsService.GetPhotoNews(requestGetPhotoNewsViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }
       
        #region Attachment

        //[Authorize]
        [HttpPost("Attachment/NewAttachment")]
        public async Task<IActionResult> NewNewsAttachment(
            [FromForm] RequestNewPhotoNewsAttachmentViewModel requestPhotoNewNewsAttachmentViewModel)
        {
            return (await _photoNewsAttachmentService.NewNewsAttachment(
                    requestPhotoNewNewsAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPut("Attachment/EditAttachment")]
        public async Task<IActionResult> EditNewsAttachment(
            [FromForm] RequestEditPhotoNewsAttachmentViewModel requestEditPhotoNewsAttachmentViewModel)
        {
            return (await _photoNewsAttachmentService.EditNewsAttachment(
                    requestEditPhotoNewsAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPost("Attachment/GetAttachment")]
        public async Task<IActionResult> GetNewsAttachment(
            [FromForm] RequestGetPhotoNewsAttachmentViewModel requestPhotoGetNewsAttachmentViewModel)
        {
            return (await _photoNewsAttachmentService.GetNewsAttachmentList(
                    requestPhotoGetNewsAttachmentViewModel))
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