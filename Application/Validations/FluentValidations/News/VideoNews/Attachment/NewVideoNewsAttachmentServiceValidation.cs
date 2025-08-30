using System.IO;
using System.Linq;
using Application.ViewModels.News.PhotoNews.Attachment.Request;
using Application.ViewModels.News.VideoNews.Attachment.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.News.VideoNews.Attachment
{
    public class NewVideoNewsAttachmentServiceValidation: AbstractValidator<RequestNewVideoNewsAttachmentViewModel>
    {
        private string[] extensions = {".mp4", ".mkv"};

        public NewVideoNewsAttachmentServiceValidation()
        {
            RuleFor(l => l.VideoPath).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید ویدئو باشد"));
        }
    }
}