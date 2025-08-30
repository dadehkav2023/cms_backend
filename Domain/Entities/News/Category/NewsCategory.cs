using System.Collections.Generic;
using Domain.Attributes;
using Domain.Attributes.News;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.News.Category
{
    [Auditable]
    [NewsEntity]
    public class NewsCategory : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }
        
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<PhotoNews.PhotoNews> PhotoNews { get; set; }
        public virtual ICollection<VideoNews.VideoNews> VideoNews { get; set; }

    }
}