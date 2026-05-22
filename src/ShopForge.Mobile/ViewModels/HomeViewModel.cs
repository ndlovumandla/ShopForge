using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Categories;
using ShopForge.Shared.DTOs.Products;

namespace ShopForge.Mobile.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    private readonly IApiService _api;

    [ObservableProperty] private ObservableCollection<BannerSlideDto> _banners = new();
    [ObservableProperty] private ObservableCollection<CategoryDto> _categories = new();
    [ObservableProperty] private ObservableCollection<ProductSummaryDto> _featuredProducts = new();
    [ObservableProperty] private ObservableCollection<ProductSummaryDto> _newArrivals = new();
    [ObservableProperty] private ObservableCollection<ProductSummaryDto> _specialOffers = new();
    [ObservableProperty] private bool _isRefreshing;

    public HomeViewModel(IApiService api) { _api = api; Title = "ShopForge"; }

    [RelayCommand]
    public async Task LoadDataAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            var bannersTask = _api.GetBannersAsync();
            var categoriesTask = _api.GetCategoriesAsync();
            var featuredTask = _api.GetFeaturedProductsAsync();
            var arrivalsTask = _api.GetNewArrivalsAsync();
            await Task.WhenAll(bannersTask, categoriesTask, featuredTask, arrivalsTask);

            var banners = await bannersTask;
            var categories = await categoriesTask;
            var featured = await featuredTask;
            var arrivals = await arrivalsTask;

            if (banners?.Data != null) { Banners.Clear(); foreach (var b in banners.Data) Banners.Add(b); }
            if (categories?.Data != null) { Categories.Clear(); foreach (var c in categories.Data.Where(c => c.IsActive)) Categories.Add(c); }
            if (featured?.Data != null) { FeaturedProducts.Clear(); foreach (var p in featured.Data) FeaturedProducts.Add(p); }
            if (arrivals?.Data != null)
            {
                NewArrivals.Clear(); SpecialOffers.Clear();
                foreach (var p in arrivals.Data)
                {
                    NewArrivals.Add(p);
                    if (p.CompareAtPrice.HasValue && p.CompareAtPrice > p.Price) SpecialOffers.Add(p);
                }
            }
        });
        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task NavigateToProductAsync(ProductSummaryDto product) => await Shell.Current.GoToAsync($"product-detail?id={product.Id}");

    [RelayCommand]
    private async Task NavigateToCategoryAsync(CategoryDto category) => await Shell.Current.GoToAsync($"//shop?categoryId={category.Id}");

    [RelayCommand]
    private async Task RefreshAsync() { IsRefreshing = true; await LoadDataAsync(); }
}
