using FluentValidation;
using ShopForge.Shared.DTOs.Orders;

namespace ShopForge.Api.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.ShippingAddressId).GreaterThan(0);
        RuleFor(x => x.ShippingMethodId).GreaterThan(0);
    }
}
