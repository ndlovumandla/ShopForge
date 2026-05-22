using FluentValidation;
using ShopForge.Shared.DTOs.Admin;

namespace ShopForge.Api.Validators;

public class CreateCouponRequestValidator : AbstractValidator<CreateCouponRequest>
{
    public CreateCouponRequestValidator()
    {
        RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
        RuleFor(x => x.DiscountType).NotEmpty().Must(t => t == "Percentage" || t == "Fixed")
            .WithMessage("Discount type must be 'Percentage' or 'Fixed'.");
        RuleFor(x => x.DiscountValue).GreaterThan(0);
        When(x => x.DiscountType == "Percentage", () =>
            RuleFor(x => x.DiscountValue).LessThanOrEqualTo(100).WithMessage("Percentage discount cannot exceed 100%."));
    }
}
