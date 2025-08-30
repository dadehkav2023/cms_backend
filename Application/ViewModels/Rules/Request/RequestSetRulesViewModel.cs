using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Rules.Request
{
    public class RequestSetRulesViewModel
    {
        [Required(ErrorMessage = "توضیحات را وارد کنید")]
        public string Description { get; set; }

    }
}