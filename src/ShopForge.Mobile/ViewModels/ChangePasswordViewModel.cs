using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Auth;

namespace ShopForge.Mobile.ViewModels;

public partial class ChangePasswordViewModel : BaseViewModel
{
    private readonly IApiService _api;

    [ObservableProperty] private string _currentPassword = string.Empty;
    [ObservableProperty] private string _newPassword = string.Empty;
    [ObservableProperty] private string _confirmPassword = string.Empty;

    public ChangePasswordViewModel(IApiService api)
    {
        _api = api;
        Title = "Change Password";
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(CurrentPassword) ||
            string.IsNullOrWhiteSpace(NewPassword) ||
            string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            ErrorMessage = "All fields are required.";
            return;
        }

        if (NewPassword.Length < 8)
        {
            ErrorMessage = "New password must be at least 8 characters.";
            return;
        }

        if (!string.Equals(NewPassword, ConfirmPassword, StringComparison.Ordinal))
        {
            ErrorMessage = "New password and confirmation do not match.";
            return;
        }

        await ExecuteSafelyAsync(async () =>
        {
            var response = await _api.ChangePasswordAsync(new ChangePasswordRequest
            {
                CurrentPassword = CurrentPassword,
                NewPassword = NewPassword,
                ConfirmNewPassword = ConfirmPassword
            });

            if (response?.Success == true)
            {
                await Shell.Current.DisplayAlertAsync("Password Updated", "Your password has been changed successfully.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                ErrorMessage = response?.Message ?? "Unable to change password.";
            }
        });
    }
}
