using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.News.TextNews.Attachment.Request
{
    public class RequestEditTextNewsAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه پیوست اجباری می باشد")]
        public int Id { get; set; }
        public IFormFile AttachmentFile { get; set; }
        public string Title { get; set; }
    }
}