using System.Collections.Generic;
using Application.Utilities;
using Common.Enum;
using Common.EnumList;

namespace Application.ViewModels.Notification.Notification.Attachment.Response
{
    public class ResponseGetNotificationAttachmentListViewModel
    {
        public List<ResponseGetNotificationAttachmentViewModel> NotificationAttachmentList { get; set; }
    }
    
    public class ResponseGetNotificationAttachmentViewModel
    {
        public int Id { get; set; }
        public string AttachmentFile { get; set; }
        public string Title { get; set; }
        public FileTypeEnum FileType { get; set; }

        public string FileTypeText => FileType.GetDescription();
    }
    
}