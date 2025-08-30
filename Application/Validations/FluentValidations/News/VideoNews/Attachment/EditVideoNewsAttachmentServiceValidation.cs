using System.IO;
using System.Linq;
using Application.ViewModels.News.PhotoNews.Attachment.Request;
using Application.ViewModels.News.VideoNews.Attachment.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.News.VideoNews.Attachment
{
    public class EditVideoNewsAttachmentServiceValidation: AbstractValidator<RequestEditVideoNewsAttachmentViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public EditVideoNewsAttachmentServiceValidation()
        {
            RuleFor(l => l.VideoPath).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید تصویر باشد"));
        }
    }
}