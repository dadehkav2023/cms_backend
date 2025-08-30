using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Article.Request
{
    public class RequestEditArticleViewModel
    {
        [Required(ErrorMessage = "شناسه را وارد کنید")]
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان را وارد کنید")]
        [MinLength(5, ErrorMessage = "حداقل طول عنوان 5 کاراکتر می باشد")]
        public string Title { get; set; }
        [Required(ErrorMessage = "خلاصه مقاله را وارد کنید")]
        public string Summary { get; set; }
        [Required(ErrorMessage = "توضیحات مقاله را وارد کنید")]
        public string Description { get; set; }
        public IFormFile ImagePath { get; set; }
        public bool IsActive { get; set; } = true;
        [Required(ErrorMessage = "سال انتشار مقاله را وارد کنید")]
        public int PublishYear { get; set; }
    }
}