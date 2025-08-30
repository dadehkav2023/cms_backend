using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Statement.Category.Request
{
    public class RequestNewStatementCategoryViewModel
    {
        [Required(ErrorMessage = "عنوان دسته بندی اجباری است")]
        [MinLength(5, ErrorMessage = "حداقل طول عنوان 5 کاراکتر می باشد.")]
        public string Title { get; set; }
    }
}