using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.News.PhotoNews.Attachment.Request
{
    public class RequestNewPhotoNewsAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه خبر اجباری می باشد")]
        public int PhotoNewsId { get; set; }
        [Required(ErrorMessage = "فایل پیوست اجباری می باشد")]

        public IFormFile ImagePath { get; set; }
        [Required(ErrorMessage = "عنوان اجباری می باشد")]
        public string Title { get; set; }
    }
    
    
}