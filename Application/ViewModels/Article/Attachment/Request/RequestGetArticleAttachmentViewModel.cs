using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Article.Attachment.Request
{
    public class RequestGetArticleAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه مقاله اجباری می باشد")]
        public int ArticleId { get; set; }
        public string Title { get; set; }
    }
}