using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.QuickAccess.Request
{
    public class RequestEditQuickAccessViewModel
    {
        [Required(ErrorMessage = "شناسه اجباری می باشد")]
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان را وارد کنید")]
        [MinLength(5, ErrorMessage = "حداقل تعداد کاراکتر ع 5 کاراکتر می باشد")]
        public string Title { get; set; }

        [Required(ErrorMessage = "لینک را وارد کنید")]
        public string Link { get; set; }
        public bool IsActive { get; set; }
    }
}