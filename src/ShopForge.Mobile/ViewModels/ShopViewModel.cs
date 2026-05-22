using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Cart;
using ShopForge.Shared.DTOs.Products;

namespace ShopForge.Mobile.ViewModels;

[QueryProperty(nameof(CategoryIdParam), "categoryId")]
public partial class ShopViewModel : BaseViewModel
{
    private readonly IApiService _api;
    private readonly ICartLocalService _cartLocal;
    private System.Timers.Timer? _searchTimer;
    private int _currentPage = 1;
    private bool _hasMore = true;

    [ObservableProperty] private ObservableCollection<ProductSummaryDto> _products = new();
    [ObservableProperty] private string _searchText = string.Empty;
    [ObservableProperty] private int? _selectedCategoryId;
    [ObservableProperty] private decimal? _minPrice;
    [ObservableProperty] private decimal? _maxPrice;
    [ObservableProperty] private string _sortBy = "newest";
    [ObservableProperty] private bool _isLoadingMore;
    [ObservableProperty] private string? _categoryIdParam;

    public ShopViewModel(IApiService api, ICartLocalService cartLocal)
    {
        _api = api; _cartLocal = cartLocal; Title = "Shop";
    }

    partial void OnCategoryIdParamChanged(string? value)
    {
        if (int.TryParse(value, out var id)) SelectedCategoryId = id;
    }

    partial void OnSearchTextChanged(string value)
    {
        _searchTimer?.Stop();
        _searchTimer = new System.Timers.Timer(400);
        _searchTimer.Elapsed += async (_, _) => { _searchTimer?.Stop(); await LoadProductsAsync(reset: true); };
        _searchTimer.Start();
    }

    [RelayCommand]
    public async Task LoadProductsAsync(bool reset = true)
    {
        if (reset) { _currentPage = 1; _hasMore = true; }
        if (!_hasMore) return;
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.GetProductsAsync(page: _currentPage, pageSize: 20, categoryId: SelectedCategoryId, minPrice: MinPrice, maxPrice: MaxPrice, search: SearchText, sort: SortBy);
            if (result?.Data != null)
            {
                if (reset) Products.Clear();
                foreach (var p in result.Data.Items) Products.Add(p);
                _hasMore = result.Data.HasNextPage;
                _currentPage++;
            }
        });
        IsLoadingMore = false;
    }

    [RelayCommand]
    private async Task LoadMoreAsync()
    {
        if (IsLoadingMore || !_hasMore) return;
        IsLoadingMore = true;
        await LoadProductsAsync(reset: false);
    }

    [RelayCommand]
    private async Task NavigateToProductAsync(ProductSummaryDto product) => await Shell.Current.GoToAsync($"product-detail?id={product.Id}");

    [RelayCommand]
    private async Task AddToCartAsync(ProductSummaryDto product)
    {
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.AddToCartAsync(new AddToCartRequest { ProductId = product.Id, Quantity = 1 });
            if (result?.Success == true && result.Data != null)
            {
                _cartLocal.SetCartItemCount(result.Data.TotalItems);
                await Shell.Current.DisplayAlertAsync("Added", $"{product.Name} added to cart.", "OK");
            }
        });
    }
}
