using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Enum;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.News.TextNews.Request
{
    public class RequestNewTextNewsViewModel
    {

        public int Priority { get; set; } = 0;

        [Required(ErrorMessage = "عنوان اجباری می باشد")]
        public string Title { get; set; }
        [Required(ErrorMessage = "تصویر اجباری می باشد")]
        public IFormFile ImagePath { get; set; }
        public bool IsActive { get; set; } = true;
        [Required(ErrorMessage = "رو تیتر اجباری می باشد")]
        public string HeadTitle { get; set; }
        [Required(ErrorMessage = "زیر تیتر اجباری می باشد")]
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "خلاصه تیتر اجباری می باشد")]
        public string SummaryTitle { get; set; }
        [Required(ErrorMessage = "خلاصه خبر اجباری می باشد")]
        public string Lead { get; set; }
        [Required(ErrorMessage = "محتوا اجباری می باشد")]
        public string Content { get; set; }
        [Required(ErrorMessage = "نوع خبر اجباری می باشد")]
        public NewsTypeEnum NewsType { get; set; }
        [Required(ErrorMessage = "الویت خبر اجباری می باشد")]
        public NewsPriority NewsPriority { get; set; }
        public string PublishedDateTime { get; set; }
        [Required(ErrorMessage = "دسته بندی خبر اجباری می باشد.")]
        public List<int> CategoriesId { get; set; }

        [Required(ErrorMessage = "وضعیت نمایش در صفحه اصلی اجباری است.")]
        public bool ShowInMainPage { get; set; }
    }
}