using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.News.PhotoNews.Attachment.Request
{
    public class RequestGetPhotoNewsAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه خبر اجباری می باشد")]
        public int PhotoNewsId { get; set; }
        public string Title { get; set; }
    }
}