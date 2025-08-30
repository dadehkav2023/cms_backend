using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.RelatedLink.Request
{
    public class RequestNewRelatedLinkViewModel
    {
        [Required(ErrorMessage = "عنوان را وارد کنید")]
        [MinLength(5, ErrorMessage = "حداقل تعداد کاراکتر ع 5 کاراکتر می باشد")]
        public string Title { get; set; }

        [Required(ErrorMessage = "لینک را وارد کنید")]
        public string Link { get; set; }

        public bool IsActive { get; set; } = true;
    }
}