using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Orders;

namespace ShopForge.Mobile.ViewModels;

public partial class StatusFilterOption : ObservableObject
{
    public string Name { get; }

    [ObservableProperty] private bool _isSelected;

    public StatusFilterOption(string name)
    {
        Name = name;
    }
}

public partial class OrdersViewModel : BaseViewModel
{
    private readonly IApiService _api;
    private readonly List<OrderSummaryDto> _allOrders = [];

    [ObservableProperty] private ObservableCollection<OrderSummaryDto> _orders = new();
    [ObservableProperty] private ObservableCollection<StatusFilterOption> _statusFilters = new();
    [ObservableProperty] private bool _isRefreshing;
    [ObservableProperty] private string _selectedStatus = "All";
    [ObservableProperty] private StatusFilterOption? _selectedFilter;

    public OrdersViewModel(IApiService api)
    {
        _api = api;
        Title = "My Orders";
        InitializeFilters();
    }

    [RelayCommand]
    public async Task LoadOrdersAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.GetOrdersAsync();
            if (result?.Data != null)
            {
                _allOrders.Clear();
                _allOrders.AddRange(result.Data.Items);
                ApplyStatusFilter();
            }
        });
        IsRefreshing = false;
    }

    partial void OnSelectedStatusChanged(string value)
    {
        ApplyStatusFilter();
    }

    partial void OnSelectedFilterChanged(StatusFilterOption? value)
    {
        if (value == null)
        {
            return;
        }

        foreach (var option in StatusFilters)
        {
            option.IsSelected = ReferenceEquals(option, value);
        }

        SelectedStatus = value.Name;
    }

    [RelayCommand]
    private void SetStatusFilter(StatusFilterOption? status)
    {
        if (status != null)
        {
            SelectedFilter = status;
        }
    }

    private void ApplyStatusFilter()
    {
        var filtered = string.Equals(SelectedStatus, "All", StringComparison.OrdinalIgnoreCase)
            ? _allOrders
            : _allOrders.Where(o => string.Equals(o.Status, SelectedStatus, StringComparison.OrdinalIgnoreCase)).ToList();

        Orders.Clear();
        foreach (var order in filtered)
        {
            Orders.Add(order);
        }
    }

    private void InitializeFilters()
    {
        foreach (var status in new[] { "All", "Pending", "Processing", "Shipped", "Delivered", "Cancelled", "Refunded" })
        {
            StatusFilters.Add(new StatusFilterOption(status));
        }

        SelectedFilter = StatusFilters.FirstOrDefault();
    }

    [RelayCommand]
    private async Task NavigateToOrderAsync(OrderSummaryDto order) => await Shell.Current.GoToAsync($"order-detail?id={order.Id}");

    [RelayCommand]
    private async Task RefreshAsync() { IsRefreshing = true; await LoadOrdersAsync(); }
}
