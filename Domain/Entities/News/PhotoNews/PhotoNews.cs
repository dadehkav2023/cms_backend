using System;
using System.Collections.Generic;
using Domain.Attributes;
using Domain.Attributes.News;
using Domain.Entities.BaseEntity;
using Domain.Entities.News.Category;

namespace Domain.Entities.News.PhotoNews
{
    [Auditable]
    [NewsEntity]
    public class PhotoNews : BaseEntityWithIdentityKey
    {
        public int Priority { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string ImagePath { get; set; }
        /// <summary>
        /// آیا در صفحه اصلی نمایش دهد؟
        /// </summary>
        public bool ShowInMainPage { get; set; }
        public bool IsActive { get; set; }

        public virtual DateTime? PublishedDateTime { get; set; }

        public virtual  ICollection<PhotoNewsAttachment> PhotoNewsAttachment { get; set; }
        public virtual ICollection<NewsCategory> NewsCategories { get; set; }

    }
    
    [Auditable]
    [NewsEntity]
    public class PhotoNewsAttachment : BaseEntityWithIdentityKey
    {
        public string ImagePath { get; set; }
        public string Title { get; set; }
        
        public int PhotoNewsId { get; set; }
        public virtual PhotoNews PhotoNews { get; set; }

    }
}