using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.News.VideoNews.Attachment.Request
{
    public class RequestNewVideoNewsAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه خبر اجباری می باشد")]
        public int VideoNewsId { get; set; }
        [Required(ErrorMessage = "فایل پیوست اجباری می باشد")]

        public IFormFile VideoPath { get; set; }
        [Required(ErrorMessage = "عنوان اجباری می باشد")]
        public string Title { get; set; }
    }
    
    
}