using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Auth;

namespace ShopForge.Mobile.ViewModels;

public partial class AccountViewModel : BaseViewModel
{
    private readonly IApiService _api;
    private readonly IAuthStateService _authState;

    [ObservableProperty] private UserProfileDto? _profile;

    public string Initials => Profile != null && Profile.FirstName.Length > 0 && Profile.LastName.Length > 0
        ? $"{Profile.FirstName[0]}{Profile.LastName[0]}"
        : "?";

    public AccountViewModel(IApiService api, IAuthStateService authState)
    {
        _api = api; _authState = authState; Title = "Account";
    }

    [RelayCommand]
    public async Task LoadProfileAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.GetProfileAsync();
            if (result?.Data != null) { Profile = result.Data; OnPropertyChanged(nameof(Initials)); }
        });
    }

    [RelayCommand] private async Task EditProfileAsync() => await Shell.Current.GoToAsync("profile-edit");
    [RelayCommand] private async Task ManageAddressesAsync() => await Shell.Current.GoToAsync("address-list");
    [RelayCommand] private async Task ChangePasswordAsync() => await Shell.Current.GoToAsync("change-password");
    [RelayCommand] private async Task NotificationsAsync() => await Shell.Current.GoToAsync("notifications");

    [RelayCommand]
    private async Task LogoutAsync()
    {
        bool confirm = await Shell.Current.DisplayAlertAsync("Logout", "Are you sure you want to log out?", "Logout", "Cancel");
        if (!confirm) return;
        var refreshToken = _authState.RefreshToken;
        if (!string.IsNullOrEmpty(refreshToken)) await _api.LogoutAsync(refreshToken);
        await _authState.ClearAuthAsync();
        if (Shell.Current is global::ShopForge.Mobile.AppShell appShell)
        {
            appShell.UpdateAuthState(false);
        }

        await Shell.Current.GoToAsync("login");
    }
}
