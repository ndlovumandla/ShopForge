using FluentValidation;
using ShopForge.Shared.DTOs.Products;

namespace ShopForge.Api.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Slug).MaximumLength(500);
        RuleFor(x => x.Slug)
            .Matches("^[a-z0-9-]+$").WithMessage("Slug can only contain lowercase letters, numbers, and hyphens.")
            .When(x => !string.IsNullOrWhiteSpace(x.Slug));
        RuleFor(x => x.SKU).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CategoryId).GreaterThan(0);
        RuleFor(x => x.LowStockThreshold).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PrimaryImageUrl)
            .MaximumLength(500)
            .Must(BeValidHttpUrl)
            .When(x => !string.IsNullOrWhiteSpace(x.PrimaryImageUrl))
            .WithMessage("Primary image must be a valid http or https URL.");
        RuleFor(x => x.PrimaryImageAltText).MaximumLength(200);
        When(x => x.CompareAtPrice.HasValue, () => RuleFor(x => x.CompareAtPrice!.Value).GreaterThan(x => x.Price));
    }

    private static bool BeValidHttpUrl(string? value) =>
        Uri.TryCreate(value, UriKind.Absolute, out var uri) &&
        (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
}
