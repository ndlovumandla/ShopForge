using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Orders;
using ShopForge.Shared.DTOs.Payments;

namespace ShopForge.Mobile.ViewModels;

[QueryProperty(nameof(AddressId), "addressId")]
[QueryProperty(nameof(ShippingMethodId), "shippingMethodId")]
[QueryProperty(nameof(ShippingCost), "shippingCost")]
[QueryProperty(nameof(OrderId), "orderId")]
public partial class CheckoutPaymentViewModel : BaseViewModel
{
    private readonly IApiService _api;
    private int _pendingOrderId;

    [ObservableProperty] private int _addressId;
    [ObservableProperty] private int _shippingMethodId;
    [ObservableProperty] private decimal _shippingCost;
    [ObservableProperty] private int _orderId;
    [ObservableProperty] private string _paymentMethod = "MockCard";
    [ObservableProperty] private string _cardHolder = string.Empty;
    [ObservableProperty] private string _cardNumber = string.Empty;
    [ObservableProperty] private string _expiryMonth = string.Empty;
    [ObservableProperty] private string _expiryYear = string.Empty;
    [ObservableProperty] private string _cvv = string.Empty;
    [ObservableProperty] private string _detectedCardBrand = string.Empty;

    public string[] DemoCards { get; } =
    [
        "4000 0000 0001 0001 - Visa (Success)",
        "5500 0000 0000 0001 - Mastercard (Success)",
        "4000 0000 0000 0002 - Visa (Declined: Insufficient funds)",
        "4000 0000 0009 0003 - Visa (Declined: Card expired)",
        "4000 0000 0008 0004 - Visa (Declined: Fraud suspected)"
    ];

    public CheckoutPaymentViewModel(IApiService api) { _api = api; Title = "Payment"; }

    partial void OnOrderIdChanged(int value)
    {
        if (value > 0)
            _pendingOrderId = value;
    }

    partial void OnCardNumberChanged(string value)
    {
        var digits = new string(value.Where(char.IsDigit).ToArray());
        if (digits.StartsWith("4")) DetectedCardBrand = "Visa";
        else if (digits.Length >= 2 && int.TryParse(digits[..2], out var mc) && mc >= 51 && mc <= 55) DetectedCardBrand = "Mastercard";
        else if (digits.StartsWith("34") || digits.StartsWith("37")) DetectedCardBrand = "Amex";
        else DetectedCardBrand = string.Empty;
    }

    [RelayCommand]
    private async Task PayAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            if (_pendingOrderId <= 0)
            {
                var orderResult = await _api.CreateOrderAsync(new CreateOrderRequest
                {
                    ShippingAddressId = AddressId,
                    ShippingMethodId = ShippingMethodId
                });

                if (orderResult?.Success != true || orderResult.Data == null)
                {
                    ErrorMessage = orderResult?.Message ?? "Failed to create order.";
                    return;
                }

                _pendingOrderId = orderResult.Data.Id;
            }

            var digits = new string(CardNumber.Where(char.IsDigit).ToArray());
            var paymentRequest = new ProcessPaymentRequest
            {
                OrderId = _pendingOrderId,
                Method = "MockCard",
                CardNumber = digits,
                ExpiryMonth = int.TryParse(ExpiryMonth, out var em) ? em : null,
                ExpiryYear = int.TryParse(ExpiryYear, out var ey) ? ey : null,
                CVV = Cvv,
                CardHolder = CardHolder
            };
            var payResult = await _api.ProcessPaymentAsync(paymentRequest);
            if (payResult?.Success == true && payResult.Data != null)
            {
                await Shell.Current.GoToAsync($"checkout/confirmation?orderId={_pendingOrderId}&success=true&transactionId={payResult.Data.TransactionId}");
                _pendingOrderId = 0;
            }
            else
                ErrorMessage = payResult?.Message ?? "Payment failed.";
        });
    }

    [RelayCommand]
    private void ApplyDemoCard(string demoCard)
    {
        var number = demoCard.Split('-')[0].Trim();
        CardHolder = "ShopForge Demo";
        CardNumber = number;
        ExpiryMonth = "12";
        ExpiryYear = DateTime.UtcNow.AddYears(2).Year.ToString();
        Cvv = "123";
    }
}
