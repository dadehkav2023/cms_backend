using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Rules.Attachment.Request
{
    public class RequestNewRulesAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه قوانین و مقررات اجباری می باشد")]
        public int RulesId { get; set; }
        [Required(ErrorMessage = "فایل پیوست اجباری می باشد")]
        public IFormFile AttachmentFile { get; set; }
        public string Title { get; set; }
    }
    
    
}