using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Rules.Attachment.Request
{
    public class RequestGetRulesAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه قوانین و مقررات اجباری می باشد")]
        public int RulesId { get; set; }
        public string Title { get; set; }
    }
}