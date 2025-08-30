using System.IO;
using System.Linq;
using Application.ViewModels.Slider;
using Application.ViewModels.Slider.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.SliderValidation
{

    public class EditSliderValidation : AbstractValidator<RequestEditSliderViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public EditSliderValidation()
        {
            RuleFor(l => l.SliderFile).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل اسلاید باید تصویر باشد"));
        }
    }

}