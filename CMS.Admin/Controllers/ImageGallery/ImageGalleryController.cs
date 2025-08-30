using System.Threading.Tasks;
using Application.Services.ImageGallery;
using Application.ViewModels.ImageGallery.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.ImageGallery
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ImageGaleryController : ControllerBase
    {
        private readonly IImageGalleryService _imageGalleryService;

        public ImageGaleryController(IImageGalleryService imageGalleryService)
        {
            this._imageGalleryService = imageGalleryService;
        }

        //[Authorize]
        [HttpPost("NewImageGallery")]
        public async Task<IActionResult> NewImageGallery(
            [FromForm] RequestNewGalleryViewModel requestNewGalleryViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _imageGalleryService.NewGallery(requestNewGalleryViewModel, userId)).ToWebApiResult()
                .ToHttpResponse();
        }
        
        //[Authorize]
        [HttpPut("EditImageGallery")]
        public async Task<IActionResult> EditImageGallery(
            [FromForm] RequestEditGalleryViewModel requestEditGalleryViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _imageGalleryService.EditGallery(requestEditGalleryViewModel, userId)).ToWebApiResult()
                .ToHttpResponse();
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
        
        //[Authorize]
        [HttpDelete("DeleteImageGallery")]
        public async Task<IActionResult> DeleteImageGallery(
            [FromForm] int galleryId)
        {
            var userId = 1;
            //todo Get UserId
            return (await _imageGalleryService.RemoveGallery(galleryId, userId)).ToWebApiResult()
                .ToHttpResponse();
        }
    }
}