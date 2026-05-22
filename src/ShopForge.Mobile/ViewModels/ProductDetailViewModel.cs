using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Cart;
using ShopForge.Shared.DTOs.Products;
using ShopForge.Shared.DTOs.Reviews;

namespace ShopForge.Mobile.ViewModels;

[QueryProperty(nameof(ProductId), "id")]
public partial class ProductDetailViewModel : BaseViewModel
{
    private readonly IApiService _api;
    private readonly ICartLocalService _cartLocal;

    [ObservableProperty] private int _productId;
    [ObservableProperty] private ProductDto? _product;
    [ObservableProperty] private ObservableCollection<ReviewDto> _reviews = new();
    [ObservableProperty] private int _quantity = 1;
    [ObservableProperty] private ProductVariantDto? _selectedVariant;
    [ObservableProperty] private bool _descriptionExpanded;

    public ProductDetailViewModel(IApiService api, ICartLocalService cartLocal)
    {
        _api = api; _cartLocal = cartLocal;
    }

    partial void OnProductIdChanged(int value)
    {
        if (value > 0) _ = LoadProductAsync();
    }

    [RelayCommand]
    private async Task LoadProductAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            var productTask = _api.GetProductByIdAsync(ProductId);
            var reviewsTask = _api.GetProductReviewsAsync(ProductId);
            await Task.WhenAll(productTask, reviewsTask);
            var product = await productTask;
            var reviews = await reviewsTask;
            if (product?.Data != null) { Product = product.Data; Title = product.Data.Name; }
            if (reviews?.Data != null) { Reviews.Clear(); foreach (var r in reviews.Data.Items) Reviews.Add(r); }
        });
    }

    [RelayCommand]
    private void IncreaseQuantity() { if (Product != null && Quantity < Product.StockQuantity) Quantity++; }

    [RelayCommand]
    private void DecreaseQuantity() { if (Quantity > 1) Quantity--; }

    [RelayCommand]
    private async Task AddToCartAsync()
    {
        if (Product == null) return;
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.AddToCartAsync(new AddToCartRequest { ProductId = Product.Id, ProductVariantId = SelectedVariant?.Id, Quantity = Quantity });
            if (result?.Success == true && result.Data != null)
            {
                _cartLocal.SetCartItemCount(result.Data.TotalItems);
                await Shell.Current.DisplayAlertAsync("Added to Cart", $"{Quantity}x {Product.Name} added to your cart.", "OK");
            }
        });
    }

    [RelayCommand]
    private async Task AddToWishlistAsync()
    {
        if (Product == null) return;
        await ExecuteSafelyAsync(async () =>
        {
            await _api.AddToWishlistAsync(Product.Id);
            await Shell.Current.DisplayAlertAsync("Wishlist", "Product saved to wishlist.", "OK");
        });
    }

    [RelayCommand]
    private void ToggleDescription() => DescriptionExpanded = !DescriptionExpanded;
}
