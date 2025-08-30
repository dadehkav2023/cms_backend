using System;
using System.ComponentModel.DataAnnotations;
using Application.Utilities;
using Common.Enum;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Notification.Notification.Attachment.Request
{
    public class RequestNewNotificationAttachmentViewModel
    {
        [Required(ErrorMessage = "شناسه اطلاعیه اجباری می باشد")]
        public int NotificationId { get; set; }
        [Required(ErrorMessage = "فایل پیوست اجباری می باشد")]
        public IFormFile AttachmentFile { get; set; }
        public string Title { get; set; }
    }
    
    
}