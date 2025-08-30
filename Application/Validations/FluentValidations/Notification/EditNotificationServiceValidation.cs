using System.IO;
using Application.ViewModels.Notification.Notification.Request;
using FluentValidation;
using System.Linq;

namespace Application.Validations.FluentValidations.Notification
{
    public class EditNotificationServiceValidation : AbstractValidator<RequestEditNotificationViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public EditNotificationServiceValidation()
        {
            RuleFor(l => l.ImagePath).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید تصویر باشد"));
        }
    }
}