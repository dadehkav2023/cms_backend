using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Public.Upload
{
    public interface IUploader
    {
        Task<string> UploadFile(IFormFile file, string path, string exteraPath);
        Task<string> UploadFile(IFormFile file, string path);
    }
}
