using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.ViewModels.ApiImageUploader;
using Application.ViewModels.UploadImage;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace Infrastructure.ExternalApi.FileServer
{
    public class FileUploaderService : IFileUploaderService
    {
        private ApiImageUploaderViewModel _settings;
        private readonly IWebHostEnvironment _environment;

        public FileUploaderService(IWebHostEnvironment hostingEnvironment, IOptions<ApiImageUploaderViewModel> settings)
        {
            _settings = settings.Value;
            _environment = hostingEnvironment;
        }

        public List<string> Upload(List<IFormFile> receivedFiles)
        {
            // var request = new RestRequest(Method.POST);
            // foreach (var item in receivedFiles)
            // {
            //     byte[] bytes;
            //     using (var ms = new MemoryStream())
            //     {
            //         item.CopyToAsync(ms);
            //         bytes = ms.ToArray();
            //     }
            //
            //     request.AddFile(item.FileName, bytes, item.FileName, item.ContentType);
            // }
            //
            // var files = request.Files;

            var result = UploadFileASync(receivedFiles).Result;
            return result.FileNameAddress;
        } 


        private async Task<UploadDto> UploadFileASync(List<IFormFile> files)
        {
            string newName = Guid.NewGuid().ToString();
            var date = DateTime.Now;
            string folder = $@"Resources\images\{date.Year}\{date.Year}-{date.Month}\";
            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            List<string> address = new List<string>();
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = newName + file.FileName;
                    var filePath = Path.Combine(uploadsRootFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    address.Add(folder + fileName);
                }
            }

            return new UploadDto()
            {
                FileNameAddress = address,
                Status = true,
            };
        }

        public async Task<IBusinessLogicResult<UploadImageViewModel>> Upload(IFormFile receivedFiles)
        {
            var messages = new List<BusinessLogicMessage>();

            List<IFormFile> listImage = new List<IFormFile>();
            listImage.Add(receivedFiles);
            var result = UploadFileASync(listImage).Result;
            var returndata= (new UploadImageViewModel()
            {
                    url = result.FileNameAddress[0],
            });
            messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                       message: MessageId.Success));
            return new BusinessLogicResult<UploadImageViewModel>(succeeded: true, result: returndata, messages: messages);
        }

        public class UploadDto
        {
            public bool Status { get; set; }
            public List<string> FileNameAddress { get; set; }
        }
    }
}