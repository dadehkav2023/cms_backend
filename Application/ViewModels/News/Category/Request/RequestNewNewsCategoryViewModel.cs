using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.News.Category.Request
{
    public class RequestNewNewsCategoryViewModel
    {
        [Required(ErrorMessage = "عنوان دسته بندی اجباری است")]
        [MinLength(5, ErrorMessage = "حداقل طول عنوان 5 کاراکتر می باشد.")]
        public string Title { get; set; }
    }
}