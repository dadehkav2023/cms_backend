using System.IO;
using System.Linq;
using Application.ViewModels.AboutUs.Request;
using Application.ViewModels.CMS.Setting.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.AboutUs
{
    public class AboutUsValidation : AbstractValidator<RequestSetAboutUsViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public AboutUsValidation()
        {
            RuleFor(l => l.HeaderImage).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید تصویر باشد"));
        }
    }
}