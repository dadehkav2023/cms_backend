using System.IO;
using System.Linq;
using Application.ViewModels.News.TextNews.Attachment.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.News.TextNews.Attachment
{
    public class NewTextNewsAttachmentServiceValidation : AbstractValidator<RequestTextNewNewsAttachmentViewModel>
    {
        private string[] extensions = {".pdf", ".doc", ".docx", ".rar", ".zip"};

        public NewTextNewsAttachmentServiceValidation()
        {
            RuleFor(l => l.AttachmentFile).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید صحیح نمباشد(فرمت های صحیح: pdf, doc, docx, rar, zip)"));
        }
    }
}