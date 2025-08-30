using System;
using System.Collections.Generic;
using Domain.Attributes;
using Domain.Attributes.Notification;
using Domain.Attributes.Statement;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.Statement
{
    [Auditable]
    [StatementEntity]
    public class Statement : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime? PublishDateTime { get; set; }

        public virtual ICollection<StatementAttachment> StatementAttachments { get; set; }
        public virtual ICollection<StatementCategory> StatementCategories { get; set; }
    }
}