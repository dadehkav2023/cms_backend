using System.IO;
using System.Linq;
using Application.ViewModels.Notification.Notification.Attachment.Request;
using Application.ViewModels.Notification.Notification.Request;
using FluentValidation;

namespace Application.Validations.FluentValidations.Notification.Attachment
{
    public class NewNotificationAttachmentServiceValidation : AbstractValidator<RequestNewNotificationAttachmentViewModel>
    {
        private string[] extensions = {".pdf", ".doc", ".docx", ".rar", ".zip"};

        public NewNotificationAttachmentServiceValidation()
        {
            RuleFor(l => l.AttachmentFile).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید صحیح نمباشد(فرمت های صحیح: pdf, doc, docx, rar, zip)"));
        }
    }
}