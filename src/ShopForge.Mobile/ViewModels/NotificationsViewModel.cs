using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Admin;

namespace ShopForge.Mobile.ViewModels;

public partial class NotificationsViewModel : BaseViewModel
{
    private readonly IApiService _api;
    [ObservableProperty] private ObservableCollection<NotificationDto> _notifications = new();

    public NotificationsViewModel(IApiService api) { _api = api; Title = "Notifications"; }

    [RelayCommand]
    public async Task LoadAsync()
    {
        var result = await _api.GetNotificationsAsync();
        if (result?.Data != null) { Notifications.Clear(); foreach (var n in result.Data) Notifications.Add(n); }
    }

    [RelayCommand]
    private async Task MarkReadAsync(NotificationDto n) { await _api.MarkNotificationReadAsync(n.Id); await LoadAsync(); }

    [RelayCommand]
    private async Task MarkAllReadAsync() { await _api.MarkAllNotificationsReadAsync(); await LoadAsync(); }

    [RelayCommand]
    private async Task DeleteAsync(NotificationDto n) { await _api.DeleteNotificationAsync(n.Id); Notifications.Remove(n); }
}
