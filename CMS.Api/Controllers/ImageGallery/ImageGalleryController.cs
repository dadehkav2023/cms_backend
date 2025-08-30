using System.Threading.Tasks;
using Application.Services.ImageGallery;
using Application.ViewModels.ImageGallery.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.ImageGallery
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageGaleryController : ControllerBase
    {
        private readonly IImageGalleryService _imageGalleryService;

        public ImageGaleryController(IImageGalleryService imageGalleryService)
        {
            this._imageGalleryService = imageGalleryService;
        }

        
        //[Authorize]
        [HttpPost("GetImageGallery")]
        public async Task<IActionResult> GetImageGallery(
            [FromBody] RequestGetGalleryViewModel requestGetGalleryViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _imageGalleryService.GetGallery(requestGetGalleryViewModel, userId)).ToWebApiResult()
                .ToHttpResponse();
        }
    }
}