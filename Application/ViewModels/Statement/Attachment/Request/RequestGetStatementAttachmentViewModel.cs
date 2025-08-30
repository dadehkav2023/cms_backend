using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Statement.Attachment.Request
{
    public class RequestGetStatementAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه بیانیه اجباری می باشد")]
        public int StatementId { get; set; }
        public string Title { get; set; }
    }
}