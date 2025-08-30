using System.IO;
using System.Linq;
using Application.ViewModels.ImageGallery.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.ImageGalleryValidation
{
    public class NewImageGalleryServiceValidation : AbstractValidator<RequestNewGalleryViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public NewImageGalleryServiceValidation()
        {
            RuleFor(l => l.ImageFile).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید تصویر باشد"));
            
        }
    }
}