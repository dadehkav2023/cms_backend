using System.IO;
using System.Linq;
using Application.ViewModels.News.PhotoNews.Attachment.Request;
using Application.ViewModels.News.PhotoNews.Request;
using Application.ViewModels.News.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.News.PhotoNews.Attachment
{
    public class EditPhotoNewsAttachmentServiceValidation: AbstractValidator<RequestEditPhotoNewsAttachmentViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public EditPhotoNewsAttachmentServiceValidation()
        {
            RuleFor(l => l.ImagePath).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید تصویر باشد"));
        }
    }
}