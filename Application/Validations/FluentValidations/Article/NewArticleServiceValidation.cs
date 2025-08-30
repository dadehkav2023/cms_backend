using System.IO;
using System.Linq;
using Application.ViewModels.Article.Attachment.Request;
using Application.ViewModels.Article.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.Article
{
    public class NewArticleServiceValidation : AbstractValidator<RequestNewArticleViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public NewArticleServiceValidation()
        {
            RuleFor(l => l.ImagePath).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower()))
                .WithMessage("فرمت فایل باید تصویر باشد"));
        }
    }
}