using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Cart;

namespace ShopForge.Mobile.ViewModels;

public partial class CartViewModel : BaseViewModel
{
    private readonly IApiService _api;
    private readonly ICartLocalService _cartLocal;

    [ObservableProperty] private CartDto? _cart;
    [ObservableProperty] private string _couponCode = string.Empty;
    [ObservableProperty] private string? _couponError;
    [ObservableProperty] private bool _isRefreshing;

    public bool IsCartEmpty => Cart == null || Cart.Items.Count == 0;

    public CartViewModel(IApiService api, ICartLocalService cartLocal)
    {
        _api = api; _cartLocal = cartLocal; Title = "My Cart";
    }

    [RelayCommand]
    public async Task LoadCartAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.GetCartAsync();
            if (result?.Data != null) { Cart = result.Data; _cartLocal.SetCartItemCount(result.Data.TotalItems); }
        });
        IsRefreshing = false;
        OnPropertyChanged(nameof(IsCartEmpty));
    }

    [RelayCommand]
    private async Task UpdateQuantityAsync((int ItemId, int Quantity) args)
    {
        await ExecuteSafelyAsync(async () =>
        {
            if (args.Quantity < 1) { await RemoveItemAsync(args.ItemId); return; }
            var result = await _api.UpdateCartItemAsync(args.ItemId, new UpdateCartItemRequest { Quantity = args.Quantity });
            if (result?.Data != null) { Cart = result.Data; _cartLocal.SetCartItemCount(result.Data.TotalItems); }
        });
    }

    [RelayCommand]
    private async Task IncreaseItemQuantityAsync(CartItemDto item)
    {
        if (item.Quantity >= item.StockQuantity)
        {
            return;
        }

        await UpdateQuantityAsync((item.Id, item.Quantity + 1));
    }

    [RelayCommand]
    private async Task DecreaseItemQuantityAsync(CartItemDto item)
    {
        await UpdateQuantityAsync((item.Id, item.Quantity - 1));
    }

    [RelayCommand]
    private async Task RemoveItemAsync(int itemId)
    {
        await ExecuteSafelyAsync(async () => { await _api.RemoveCartItemAsync(itemId); await LoadCartAsync(); });
    }

    [RelayCommand]
    private async Task ApplyCouponAsync()
    {
        CouponError = null;
        if (string.IsNullOrWhiteSpace(CouponCode)) { CouponError = "Enter a coupon code."; return; }
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.ApplyCouponAsync(CouponCode);
            if (result?.Success == true && result.Data != null) Cart = result.Data;
            else CouponError = result?.Message ?? "Invalid coupon code.";
        });
    }

    [RelayCommand]
    private async Task RemoveCouponAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.RemoveCouponAsync();
            if (result?.Data != null)
            {
                Cart = result.Data;
                CouponCode = string.Empty;
                CouponError = null;
            }
        });
    }

    [RelayCommand]
    private async Task CheckoutAsync()
    {
        if (IsCartEmpty) return;
        await Shell.Current.GoToAsync("checkout/address");
    }
}
