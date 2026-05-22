using FluentValidation;
using ShopForge.Shared.DTOs.Payments;

namespace ShopForge.Api.Validators;

public class ProcessPaymentRequestValidator : AbstractValidator<ProcessPaymentRequest>
{
    public ProcessPaymentRequestValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0);
        RuleFor(x => x.Method).NotEmpty();

        When(x => x.Method == "MockCard", () =>
        {
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage("Card number is required.");
            RuleFor(x => x.ExpiryMonth).InclusiveBetween(1, 12).WithMessage("Invalid expiry month.");
            RuleFor(x => x.ExpiryYear).GreaterThan(0).WithMessage("Invalid expiry year.");
            RuleFor(x => x.CVV).NotEmpty().MinimumLength(3).MaximumLength(4).WithMessage("Invalid CVV.");
            RuleFor(x => x.CardHolder).NotEmpty().WithMessage("Card holder name is required.");
        });
    }
}
