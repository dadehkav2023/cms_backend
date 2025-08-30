using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Notification.Notification.Attachment.Request
{
    public class RequestGetNotificationAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه اطلاعیه اجباری می باشد")]
        public int NotificationId { get; set; }
        public string Title { get; set; }
    }
}