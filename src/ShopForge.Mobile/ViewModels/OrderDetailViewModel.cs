using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Cart;
using ShopForge.Shared.DTOs.Orders;

namespace ShopForge.Mobile.ViewModels;

[QueryProperty(nameof(OrderId), "id")]
public partial class OrderDetailViewModel : BaseViewModel
{
    private readonly IApiService _api;
    [ObservableProperty] private int _orderId;
    [ObservableProperty] private OrderDto? _order;

    public bool CanPayOrder => Order != null &&
                               string.Equals(Order.Status, "Pending", StringComparison.OrdinalIgnoreCase) &&
                               !string.Equals(Order.Payment?.Status, "Completed", StringComparison.OrdinalIgnoreCase);

    public bool CanCancelOrder => Order != null &&
                                  string.Equals(Order.Status, "Pending", StringComparison.OrdinalIgnoreCase);

    public bool CanReorder => Order != null &&
                              string.Equals(Order.Status, "Delivered", StringComparison.OrdinalIgnoreCase);

    partial void OnOrderChanged(OrderDto? value)
    {
        OnPropertyChanged(nameof(CanPayOrder));
        OnPropertyChanged(nameof(CanCancelOrder));
        OnPropertyChanged(nameof(CanReorder));
    }

    public OrderDetailViewModel(IApiService api) { _api = api; Title = "Order Details"; }

    partial void OnOrderIdChanged(int value) { if (value > 0) _ = LoadOrderAsync(); }

    [RelayCommand]
    private async Task LoadOrderAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.GetOrderByIdAsync(OrderId);
            if (result?.Data != null) { Order = result.Data; Title = $"Order #{result.Data.OrderNumber}"; }
        });
    }

    [RelayCommand]
    private async Task PayOrderAsync()
    {
        if (Order == null || !CanPayOrder)
        {
            ErrorMessage = "This order is not eligible for payment.";
            return;
        }

        await Shell.Current.GoToAsync($"checkout/payment?orderId={Order.Id}");
    }

    [RelayCommand]
    private async Task CancelOrderAsync()
    {
        if (Order == null) return;
        if (!CanCancelOrder)
        {
            ErrorMessage = "Only pending orders can be cancelled.";
            return;
        }

        bool confirm = await Shell.Current.DisplayAlertAsync("Cancel Order", "Are you sure you want to cancel this order?", "Yes", "No");
        if (!confirm) return;
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.CancelOrderAsync(Order.Id, "Customer request");
            if (result?.Success == true) await LoadOrderAsync();
            else ErrorMessage = result?.Message ?? "Could not cancel order.";
        });
    }

    [RelayCommand]
    private async Task ReorderAsync()
    {
        if (Order == null)
        {
            return;
        }

        await ExecuteSafelyAsync(async () =>
        {
            foreach (var item in Order.Items)
            {
                await _api.AddToCartAsync(new AddToCartRequest
                {
                    ProductId = item.ProductId,
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity
                });
            }

            await Shell.Current.DisplayAlertAsync("Reorder", "Items were added back to your cart.", "OK");
            await Shell.Current.GoToAsync("//cart");
        });
    }
}
