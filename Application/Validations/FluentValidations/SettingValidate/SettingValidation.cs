using System.IO;
using System.Linq;
using Application.ViewModels.CMS.Setting.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.SettingValidate
{
    public class SettingValidation : AbstractValidator<RequestSetSettingViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public SettingValidation()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("نام سامانه اجباری می باشد").Length(5, 500)
                .WithMessage("حداقل 5 کارامتر و حداکثر 500 کاراکتر");
            RuleFor(p => p.Address).NotNull().WithMessage("آدرس اجباری می باشد").Length(5, 4000)
                .WithMessage("طول آدرس حداقل 5 و حداکثر 4000 می باشد");
            RuleFor(l => l.LogoImageAddress).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت لوگو باید تصویر باشد"));
        }
    }
}