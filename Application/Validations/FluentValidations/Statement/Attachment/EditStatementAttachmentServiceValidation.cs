using System.IO;
using System.Linq;
using Application.ViewModels.Statement.Attachment.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.Statement.Attachment
{
    public class EditStatementAttachmentServiceValidation : AbstractValidator<RequestEditStatementAttachmentViewModel>
    {
        private string[] extensions = {".pdf", ".doc", ".docx", ".rar", ".zip"};

        public EditStatementAttachmentServiceValidation()
        {
            RuleFor(l => l.AttachmentFile).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید صحیح نمباشد(فرمت های صحیح: pdf, doc, docx, rar, zip)"));
        }
    }
}