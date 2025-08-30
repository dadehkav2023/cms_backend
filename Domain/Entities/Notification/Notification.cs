using System.Collections.Generic;
using Domain.Attributes;
using Domain.Attributes.Notification;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.Notification
{
    [Auditable]
    [NotificationEntity]
    public class Notification : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<NotificationAttachment> NotificationAttachments { get; set; }
    }
}