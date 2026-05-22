using FluentValidation;
using ShopForge.Shared.DTOs.Reviews;

namespace ShopForge.Api.Validators;

public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
{
    public CreateReviewRequestValidator()
    {
        RuleFor(x => x.Rating).InclusiveBetween(1, 5);
        RuleFor(x => x.Title).MaximumLength(200).When(x => x.Title != null);
        RuleFor(x => x.Body).MaximumLength(2000).When(x => x.Body != null);
    }
}
