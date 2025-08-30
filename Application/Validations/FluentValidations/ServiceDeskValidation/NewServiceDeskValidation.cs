using System.IO;
using System.Linq;
using Application.ViewModels.ServiceDesk.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.ServiceDeskValidation
{
    public class NewServiceDeskValidation : AbstractValidator<RequestNewServiceDeskViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public NewServiceDeskValidation()
        {
            RuleFor(l => l.ImageFile).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید تصویر باشد"));
        }
    }
}