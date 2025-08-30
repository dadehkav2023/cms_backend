using System.Threading.Tasks;
using Application.Services.News.PhotoNews;
using Application.Services.News.PhotoNews;
using Application.Services.News.PhotoNews.Attachment;
using Application.ViewModels.News.PhotoNews.Request;
using Application.ViewModels.News.PhotoNews.Attachment.Request;
using Application.ViewModels.News.PhotoNews.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.News
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
        [HttpPost("Attachment/GetAttachment")]
        public async Task<IActionResult> GetNewsAttachment(
            [FromForm] RequestGetPhotoNewsAttachmentViewModel requestPhotoGetNewsAttachmentViewModel)
        {
            return (await _photoNewsAttachmentService.GetNewsAttachmentList(
                    requestPhotoGetNewsAttachmentViewModel))
                .ToWebApiResult()
                .ToHttpResponse();
        }


        #endregion
    }
}