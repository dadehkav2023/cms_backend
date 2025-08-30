using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.News.TextNews.Attachment.Request
{
    public class RequestTextNewNewsAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه خبر اجباری می باشد")]
        public int NewsId { get; set; }
        [Required(ErrorMessage = "فایل پیوست اجباری می باشد")]
        public IFormFile AttachmentFile { get; set; }
        public string Title { get; set; }
    }
    
    
}