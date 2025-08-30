using System.IO;
using System.Linq;
using Application.ViewModels.Rules.Attachment.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.Rules.Attachment
{
    public class EditRulesAttachmentServiceValidation : AbstractValidator<RequestEditRulesAttachmentViewModel>
    {
        private string[] extensions = {".pdf", ".doc", ".docx", ".rar", ".zip"};

        public EditRulesAttachmentServiceValidation()
        {
            RuleFor(l => l.AttachmentFile).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید صحیح نمباشد(فرمت های صحیح: pdf, doc, docx, rar, zip)"));
        }
    }
}