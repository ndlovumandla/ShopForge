using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Orders;

namespace ShopForge.Mobile.ViewModels;

[QueryProperty(nameof(OrderId), "orderId")]
[QueryProperty(nameof(IsSuccess), "success")]
[QueryProperty(nameof(TransactionId), "transactionId")]
public partial class CheckoutConfirmationViewModel : BaseViewModel
{
    private readonly IApiService _api;
    [ObservableProperty] private int _orderId;
    [ObservableProperty] private bool _isSuccess = true;
    [ObservableProperty] private string? _transactionId;
    [ObservableProperty] private OrderDto? _order;

    public CheckoutConfirmationViewModel(IApiService api) { _api = api; }

    partial void OnOrderIdChanged(int value) { if (value > 0) _ = LoadOrderAsync(); }

    [RelayCommand]
    private async Task LoadOrderAsync()
    {
        var result = await _api.GetOrderByIdAsync(OrderId);
        if (result?.Data != null) Order = result.Data;
    }

    [RelayCommand]
    private async Task TrackOrderAsync() => await Shell.Current.GoToAsync($"order-detail?id={OrderId}");

    [RelayCommand]
    private async Task ContinueShoppingAsync() => await Shell.Current.GoToAsync("//home");
}
