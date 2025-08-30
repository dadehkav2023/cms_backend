using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Enum;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.News.Request
{
    public class RequestEditNewsViewModel
    {
        public int Priority { get; set; } = 0;

        public int Id { get; set; }

        public string Title { get; set; }

        public IFormFile ImagePath { get; set; }
        public bool IsActive { get; set; } = true;

        public string HeadTitle { get; set; }

        public string SubTitle { get; set; }

        public string SummaryTitle { get; set; }

        public string Lead { get; set; }

        public string Content { get; set; }
        
        public string Summary { get; set; }

        public NewsTypeEnum NewsType { get; set; }

        public NewsPriority NewsPriority { get; set; }

        public string PublishedDateTime { get; set; }

        public List<int> CategoriesId { get; set; }

        [Required(ErrorMessage = "نوع محتوای خبر الزامی است")]
        public NewsContentTypeEnum NewsContentType { get; set; }
        
        [Required(ErrorMessage = "وضعیت نمایش در صفحه اصلی الزامی است")]
        public bool ShowInMainPage { get; set; }

    }
}