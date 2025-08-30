using System.ComponentModel.DataAnnotations;
using Common.Enum;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Notification.Notification.Attachment.Request
{
    public class RequestEditNotificationAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه پیوست اجباری می باشد")]
        public int Id { get; set; }
        public IFormFile AttachmentFile { get; set; }
        public string Title { get; set; }
    }
}