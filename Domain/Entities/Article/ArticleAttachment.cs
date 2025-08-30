using Common.Enum;
using Domain.Attributes;
using Domain.Attributes.Notification;
using Domain.Attributes.Article;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.Article
{
    [ArticleEntity]
    [Auditable]
    public class ArticleAttachment : BaseEntityWithIdentityKey
    {
        public string AttachmentFile { get; set; }
        public string Title { get; set; }
        public FileTypeEnum FileType { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}