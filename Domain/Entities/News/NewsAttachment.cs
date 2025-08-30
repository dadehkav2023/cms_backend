using Common.Enum;
using Domain.Attributes;
using Domain.Attributes.News;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.News
{
    [NewsEntity]
    [Auditable]
    public class NewsAttachment : BaseEntityWithIdentityKey
    {
        public string AttachmentFile { get; set; }
        public string Title { get; set; }
        public FileTypeEnum FileType { get; set; }
        public int NewsId { get; set; }
        public virtual News News { get; set; }
    }
}