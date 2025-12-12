using Application.ViewModels.Store.Product;
using FluentValidation;

namespace Application.Validations.FluentValidations.Store;

public class RequestSetProductValidator : AbstractValidator<RequestSetProductViewModel>
{
    public RequestSetProductValidator()
    {
        RuleFor(x => x.ProductTypeEnum)
            .IsInEnum()
            .WithMessage("نوع محصول درست نمی باشد");

        RuleFor(x => x.Title)
            .MaximumLength(300)
            .WithMessage("حداکثر تعداد کاراکتر عنوان محصول 300 تا باید باشد")
            .NotEmpty()
            .WithMessage("عنوان محصول اجباری می باشد");
    }
}