using System.IO;
using System.Linq;
using Application.ViewModels.ImageGallery.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.ImageGalleryValidation
{
    public class EditImageGalleryServiceValidation : AbstractValidator<RequestEditGalleryViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public EditImageGalleryServiceValidation()
        {
            RuleFor(l => l.ImageFile).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید تصویر باشد"));
            
        }
    }
}