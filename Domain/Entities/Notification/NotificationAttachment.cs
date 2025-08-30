using System;
using Common.Enum;
using Domain.Attributes;
using Domain.Attributes.Notification;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.Notification
{
    [NotificationEntity]
    [Auditable]
    public class NotificationAttachment : BaseEntityWithIdentityKey
    {
        public string AttachmentFile { get; set; }
        public string Title { get; set; }
        public FileTypeEnum FileType { get; set; }
        public int NotificationId { get; set; }
        public virtual Notification Notification { get; set; }
    }
}