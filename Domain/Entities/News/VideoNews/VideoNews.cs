using System;
using System.Collections.Generic;
using Domain.Attributes;
using Domain.Attributes.News;
using Domain.Entities.BaseEntity;
using Domain.Entities.News.Category;

namespace Domain.Entities.News.VideoNews
{
    [Auditable]
    [NewsEntity]
    public class VideoNews : BaseEntityWithIdentityKey
    {
        public int Priority { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive{ get; set; }
        /// <summary>
        /// آیا در صفحه اصلی نمایش دهد؟
        /// </summary>
        public bool ShowInMainPage { get; set; }

        public virtual DateTime? PublishedDateTime { get; set; }

        public virtual  ICollection<VideoNewsAttachment> VideoNewsAttachment { get; set; }
        public virtual ICollection<NewsCategory> NewsCategories { get; set; }
    }
    
    [Auditable]
    [NewsEntity]
    public class VideoNewsAttachment : BaseEntityWithIdentityKey
    {
        public string VideoPath { get; set; }
        public string Title { get; set; }
        
        public int VideoNewsId { get; set; }
        public virtual VideoNews VideoNews { get; set; }

    }
}
