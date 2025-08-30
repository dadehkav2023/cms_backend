using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.News.VideoNews.Attachment.Request
{
    public class RequestEditVideoNewsAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه پیوست اجباری می باشد")]
        public int Id { get; set; }
        public IFormFile VideoPath { get; set; }
        
        [Required(ErrorMessage = "عنوان اجباری می باشد")]
        public string Title { get; set; }
    }
}