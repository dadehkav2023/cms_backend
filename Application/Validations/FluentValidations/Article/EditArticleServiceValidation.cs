using System.IO;
using System.Linq;
using Application.ViewModels.Article.Attachment.Request;
using Application.ViewModels.Article.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.Article
{
    public class EditArticleServiceValidation : AbstractValidator<RequestEditArticleViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public EditArticleServiceValidation()
        {
            RuleFor(l => l.ImagePath).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower()))
                .WithMessage("فرمت فایل باید تصویر باشد"));
        }
    }
}