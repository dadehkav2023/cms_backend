using System.ComponentModel.DataAnnotations;
using Common.Enum;

namespace Application.ViewModels.News.Request
{
    public class RequestDeleteNewsViewModel
    {
        public int NewsId { get; set; }

        [Required(ErrorMessage = "نوع محتوای خبر الزامی است")]
        public NewsContentTypeEnum NewsContentType { get; set; }
    }
}