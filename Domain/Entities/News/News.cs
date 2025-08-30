using System;
using System.Collections.Generic;
using Common.Enum;
using Domain.Attributes;
using Domain.Attributes.News;
using Domain.Entities.BaseEntity;
using Domain.Entities.News.Category;

namespace Domain.Entities.News
{
    [Auditable]
    [NewsEntity]
    public class News : BaseEntityWithIdentityKey
    {
        public int Priority { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// آیا در صفحه اصلی نمایش دهد؟
        /// </summary>
        public bool ShowInMainPage { get; set; }

        public string HeadTitle { get; set; }
        public string SubTitle { get; set; }
        public string SummaryTitle { get; set; }
        public string Lead { get; set; }
        public string Content { get; set; }
        public virtual NewsTypeEnum NewsType { get; set; }
        public virtual NewsPriority NewsPriority { get; set; }
        public virtual DateTime? PublishedDateTime { get; set; }

        public virtual ICollection<NewsAttachment> NewsAttachments { get; set; }
        public virtual ICollection<NewsCategory> NewsCategories { get; set; }

    }
}