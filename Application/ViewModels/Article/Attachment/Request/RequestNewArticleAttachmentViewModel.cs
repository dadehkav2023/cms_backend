using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Article.Attachment.Request
{
    public class RequestNewArticleAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه مقاله اجباری می باشد")]
        public int ArticleId { get; set; }
        [Required(ErrorMessage = "فایل پیوست اجباری می باشد")]
        public IFormFile AttachmentFile { get; set; }
        public string Title { get; set; }
    }
    
    
}