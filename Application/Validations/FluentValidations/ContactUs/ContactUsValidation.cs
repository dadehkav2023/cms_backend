using System.IO;
using System.Linq;
using Application.ViewModels.ContactUs.ContactUs.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.ContactUs
{
    public class ContactUsValidation : AbstractValidator<RequestSetContactUsViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public ContactUsValidation()
        {
            RuleFor(l => l.HeaderImage).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید تصویر باشد"));
        }
    }
}