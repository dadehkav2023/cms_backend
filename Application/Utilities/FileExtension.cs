using System.IO;
using Common.Enum;
using Microsoft.AspNetCore.Http;

namespace Application.Utilities
{
    public static class FileExtension
    {
        public static FileTypeEnum GetFileExtension(this IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            return extension switch
            {
                ".pdf" => FileTypeEnum.Pdf,
                ".doc" => FileTypeEnum.Doc,
                ".docx" => FileTypeEnum.Docx,
                ".rar" => FileTypeEnum.Rar,
                ".zip" => FileTypeEnum.Zip,
                _ => FileTypeEnum.Unknown
            };
        }
    }
}