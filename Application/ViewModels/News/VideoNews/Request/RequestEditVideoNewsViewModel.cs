using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.News.VideoNews.Request
{
    public class RequestEditVideoNewsViewModel
    {
        public int Priority { get; set; }

        [Required(ErrorMessage = "شناسه خبر اجباری است")]
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان اجباری است")]
        public string Title { get; set; }

        [Required(ErrorMessage = "خلاصه اجباری است")]
        [MaxLength(2000, ErrorMessage = "حداکثر طول خلاصه 2000 کاراکتر می باشد")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "تصویر اجباری است")]
        public IFormFile ImagePath { get; set; }

        public string PublishedDateTime { get; set; }

        public List<int> CategoriesId { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        [Required(ErrorMessage = "وضعیت نمایش در صفحه اصلی اجباری است.")]
        public bool ShowInMainPage { get; set; }

    }
}