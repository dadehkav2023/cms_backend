using System.IO;
using Application.ViewModels.Notification.Notification.Request;
using FluentValidation;
using System.Linq;

namespace Application.Validations.FluentValidations.Notification
{
    public class NewNotificationServiceValidation : AbstractValidator<RequestNewNotificationViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public NewNotificationServiceValidation()
        {
            RuleFor(l => l.ImagePath).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید تصویر باشد"));
        }
    }
}