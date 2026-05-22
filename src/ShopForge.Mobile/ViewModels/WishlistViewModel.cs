using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Products;

namespace ShopForge.Mobile.ViewModels;

public partial class WishlistViewModel : BaseViewModel
{
    private readonly IApiService _api;
    [ObservableProperty] private ObservableCollection<ProductSummaryDto> _products = new();

    public WishlistViewModel(IApiService api) { _api = api; Title = "Wishlist"; }

    [RelayCommand]
    public async Task LoadWishlistAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.GetWishlistAsync();
            if (result?.Data != null) { Products.Clear(); foreach (var p in result.Data) Products.Add(p); }
        });
    }

    [RelayCommand]
    private async Task RemoveAsync(ProductSummaryDto product)
    {
        await ExecuteSafelyAsync(async () => { await _api.RemoveFromWishlistAsync(product.Id); Products.Remove(product); });
    }

    [RelayCommand]
    private async Task MoveToCartAsync(ProductSummaryDto product)
    {
        await ExecuteSafelyAsync(async () =>
        {
            await _api.MoveToCartAsync(product.Id);
            Products.Remove(product);
            await Shell.Current.DisplayAlertAsync("Cart", $"{product.Name} moved to cart.", "OK");
        });
    }

    [RelayCommand]
    private async Task MoveAllToCartAsync()
    {
        if (Products.Count == 0)
        {
            return;
        }

        await ExecuteSafelyAsync(async () =>
        {
            var copy = Products.ToList();
            foreach (var product in copy)
            {
                await _api.MoveToCartAsync(product.Id);
            }

            Products.Clear();
            await Shell.Current.DisplayAlertAsync("Cart", "All wishlist items were moved to cart.", "OK");
        });
    }

    [RelayCommand]
    private async Task NavigateToProductAsync(ProductSummaryDto product) => await Shell.Current.GoToAsync($"product-detail?id={product.Id}");
}
