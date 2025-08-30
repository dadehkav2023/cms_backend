using System.Collections.Generic;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.UploadImage;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.ExternalApi.FileService
{
    public interface IFileUploaderService
    {
        List<string> Upload(List<IFormFile> receivedFiles);
        Task<IBusinessLogicResult<UploadImageViewModel>> Upload(IFormFile receivedFiles);
    }

   

}
