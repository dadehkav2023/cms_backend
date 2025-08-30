using Common.Enum;
using Domain.Attributes;
using Domain.Attributes.Notification;
using Domain.Attributes.Statement;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.Statement
{
    [StatementEntity]
    [Auditable]
    public class StatementAttachment : BaseEntityWithIdentityKey
    {
        public string AttachmentFile { get; set; }
        public string Title { get; set; }
        public FileTypeEnum FileType { get; set; }
        public int StatementId { get; set; }
        public virtual Statement Statement { get; set; }
    }
}