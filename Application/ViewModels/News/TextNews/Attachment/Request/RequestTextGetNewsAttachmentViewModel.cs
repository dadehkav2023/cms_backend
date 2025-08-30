using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.News.TextNews.Attachment.Request
{
    public class RequestTextGetNewsAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه خبر اجباری می باشد")]
        public int NewsId { get; set; }
        public string Title { get; set; }
    }
}