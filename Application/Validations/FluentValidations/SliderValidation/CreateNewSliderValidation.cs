using System.IO;
using System.Linq;
using Application.ViewModels.Slider;
using Application.ViewModels.Slider.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.SliderValidation
{

    public class CreateNewSliderValidation : AbstractValidator<RequestCreateSliderViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public CreateNewSliderValidation()
        {
            //RuleFor(l => l.SliderFile).ChildRules(c => c.RuleFor(x => x.FileName)
            //    .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل اسلاید باید تصویر باشد"));
        }
    }

}