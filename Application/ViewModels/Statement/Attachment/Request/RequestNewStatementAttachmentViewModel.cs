using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Statement.Attachment.Request
{
    public class RequestNewStatementAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه بیانیه اجباری می باشد")]
        public int StatementId { get; set; }
        [Required(ErrorMessage = "فایل پیوست اجباری می باشد")]
        public IFormFile AttachmentFile { get; set; }
        public string Title { get; set; }
    }
    
    
}