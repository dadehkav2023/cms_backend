using Application.Interfaces.Public.Upload;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Public.Upload
{
    public class Uploader : IUploader
    {
        public async Task<string> UploadFile(IFormFile file, string firstpath, string exteraPath)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid() + extension;
            var tempPath = Path.Combine(firstpath, exteraPath);
            var fileFullPath = Path.Combine(tempPath, fileName);
            if (!Directory.Exists(Path.Combine(firstpath, exteraPath)))
            {
                Directory.CreateDirectory(Path.Combine(firstpath, exteraPath));
            }
            switch (extension)
            {
                default:
                    using (var stream = File.Create(fileFullPath))
                    {
                        await file.CopyToAsync(stream);
                    };
                    break;
            }
            return fileName;
        } 
        
        public async Task<string> UploadFile(IFormFile file, string firstpath)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid() + extension;
            var tempPath = Path.Combine(firstpath);
            var fileFullPath = Path.Combine(tempPath, fileName);
            if (!Directory.Exists(Path.Combine(firstpath)))
            {
                Directory.CreateDirectory(Path.Combine(firstpath));
            }
            switch (extension)
            {
                default:
                    using (var stream = File.Create(fileFullPath))
                    {
                        await file.CopyToAsync(stream);
                    };
                    break;
            }
            return fileName;
        }
    }
}
