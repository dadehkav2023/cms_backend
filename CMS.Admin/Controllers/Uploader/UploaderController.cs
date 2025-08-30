using Application.Interfaces.ExternalApi.FileService;
using Application.ViewModels.test;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Admin.Controllers.Uploader
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {
        private readonly IFileUploaderService fileUploaderService;

        public UploaderController(IFileUploaderService fileUploaderService)
        {
            this.fileUploaderService = fileUploaderService;
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile Image)
        {
            return (await fileUploaderService.Upload(Image)).ToWebApiResult().ToHttpResponse();
        }
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            return (await fileUploaderService.Upload(file)).ToWebApiResult().ToHttpResponse();
        }

        [HttpPost("UploadFileCKeeditor")]
        public async Task<IActionResult> UploadFileCKeeditor(IFormFile file)
        {
            var Upload_Data = await fileUploaderService.Upload(file);
            var _data = new List<URLImage>();
            _data.Add(new URLImage()
            {
                url = Upload_Data.Result.url,
                name = file.Name,
                size = file.Length.ToString(),
            });
            //return (await fileUploaderService.Upload(file)).ToWebApiResult().ToHttpResponse();
            var returnData = new ResultUploadFile()
            {
                //errorMessage = "NoError",
                result = _data,
            };
            return Ok(returnData);
        }
        
    }
}
