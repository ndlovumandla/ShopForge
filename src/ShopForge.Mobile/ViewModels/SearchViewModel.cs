using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Products;

namespace ShopForge.Mobile.ViewModels;

public partial class SearchViewModel : BaseViewModel
{
    private readonly IApiService _api;
    private System.Timers.Timer? _debounce;

    [ObservableProperty] private ObservableCollection<ProductSummaryDto> _results = new();
    [ObservableProperty] private ObservableCollection<string> _recentSearches = new();
    [ObservableProperty] private string _query = string.Empty;

    public SearchViewModel(IApiService api) { _api = api; Title = "Search"; LoadRecentSearches(); }

    partial void OnQueryChanged(string value)
    {
        _debounce?.Stop();
        if (value.Length < 2) { Results.Clear(); return; }
        _debounce = new System.Timers.Timer(400);
        _debounce.Elapsed += async (_, _) => { _debounce?.Stop(); await SearchAsync(); };
        _debounce.Start();
    }

    private async Task SearchAsync()
    {
        var result = await _api.SearchProductsAsync(Query);
        if (result?.Data != null)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Results.Clear();
                foreach (var p in result.Data.Items) Results.Add(p);
            });

            SaveRecentSearch(Query);
        }
    }

    private void LoadRecentSearches()
    {
        var json = Preferences.Get("recent_searches", "[]");
        var searches = JsonSerializer.Deserialize<List<string>>(json) ?? new();
        RecentSearches.Clear();
        foreach (var s in searches.Take(10)) RecentSearches.Add(s);
    }

    private void SaveRecentSearch(string query)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
        {
            return;
        }

        var existing = RecentSearches.Where(x => !string.Equals(x, query, StringComparison.OrdinalIgnoreCase)).ToList();
        existing.Insert(0, query.Trim());
        var top10 = existing.Take(10).ToList();

        RecentSearches.Clear();
        foreach (var item in top10)
        {
            RecentSearches.Add(item);
        }

        Preferences.Set("recent_searches", JsonSerializer.Serialize(top10));
    }

    [RelayCommand]
    private async Task SelectRecentAsync(string q) { Query = q; await SearchAsync(); }

    [RelayCommand]
    private void RemoveRecentAsync(string q)
    {
        RecentSearches.Remove(q);
        Preferences.Set("recent_searches", JsonSerializer.Serialize(RecentSearches.ToList()));
    }

    [RelayCommand]
    private void ClearAllRecent()
    {
        RecentSearches.Clear();
        Preferences.Set("recent_searches", "[]");
    }

    [RelayCommand]
    private async Task NavigateToProductAsync(ProductSummaryDto p) => await Shell.Current.GoToAsync($"product-detail?id={p.Id}");
}
