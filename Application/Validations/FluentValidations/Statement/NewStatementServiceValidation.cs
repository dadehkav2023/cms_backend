using System.IO;
using FluentValidation;
using System.Linq;
using Application.ViewModels.Statement.Request;

namespace Application.Validations.FluentValidations.Statement
{
    public class NewStatementServiceValidation : AbstractValidator<RequestNewStatementViewModel>
    {
        private string[] extensions = {".png", ".jpg", ".jpeg"};

        public NewStatementServiceValidation()
        {
            RuleFor(l => l.ImagePath).ChildRules(c => c.RuleFor(x => x.FileName)
                .Must(f => extensions.Contains(Path.GetExtension(f).ToLower())).WithMessage("فرمت فایل باید تصویر باشد"));
        }
    }
}