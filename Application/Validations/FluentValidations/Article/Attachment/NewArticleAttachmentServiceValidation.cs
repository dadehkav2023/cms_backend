using System.IO;
using System.Linq;
using Application.ViewModels.Article.Attachment.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.Article.Attachment
{
    public class NewArticleAttachmentServiceValidation : AbstractValidator<RequestNewArticleAttachmentViewModel>
    {
        private string[] extensions = {".pdf", ".doc", ".docx", ".rar", ".zip"};

        public NewArticleAttachmentServiceValidation()
        {
            RuleFor(l => l.AttachmentFile).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower()))
                .WithMessage("فرمت فایل باید صحیح نمباشد(فرمت های صحیح: pdf, doc, docx, rar, zip)"));
        }
    }
}