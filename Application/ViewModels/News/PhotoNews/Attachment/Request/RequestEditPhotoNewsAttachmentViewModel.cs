using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.News.PhotoNews.Attachment.Request
{
    public class RequestEditPhotoNewsAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه پیوست اجباری می باشد")]
        public int Id { get; set; }
        public IFormFile ImagePath { get; set; }
        
        [Required(ErrorMessage = "عنوان اجباری می باشد")]
        public string Title { get; set; }
    }
}