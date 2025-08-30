using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.News.Category.Request
{
    public class RequestEditNewsCategoryViewModel
    {
        [Required(ErrorMessage = "شناسه دسته بندی اجباری می باشد")]
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان دسته بندی اجباری است")]
        [MinLength(5, ErrorMessage = "حداقل طول عنوان دسته بندی 5 کاراکتر می باشد.")]
        public string Title { get; set; }
    }
}