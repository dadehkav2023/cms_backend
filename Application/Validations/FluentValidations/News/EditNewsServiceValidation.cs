using System.IO;
using System.Linq;
using Application.ViewModels.News.Request;
using Application.ViewModels.News.TextNews.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.News
{
    public class EditNewsServiceValidation: AbstractValidator<RequestEditNewsViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public EditNewsServiceValidation()
        {
            RuleFor(l => l.ImagePath).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید تصویر باشد"));
        }
    }
}