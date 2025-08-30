using System.Collections.Generic;
using Domain.Attributes;
using Domain.Attributes.Article;
using Domain.Entities.BaseEntity;
using Domain.Entities.Article;

namespace Domain.Entities.Article
{
    [Auditable]
    [ArticleEntity]
    public class Article : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public int PublishYear { get; set; }

        public virtual ICollection<ArticleAttachment> ArticleAttachments { get; set; }
    }
}