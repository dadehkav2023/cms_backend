using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.News.VideoNews.Attachment.Request
{
    public class RequestGetVideoNewsAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه خبر اجباری می باشد")]
        public int VideoNewsId { get; set; }
        public string Title { get; set; }
    }
}