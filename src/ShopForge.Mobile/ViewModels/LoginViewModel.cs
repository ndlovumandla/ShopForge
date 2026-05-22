using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Auth;

namespace ShopForge.Mobile.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IApiService _api;
    private readonly IAuthStateService _authState;

    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private bool _showPassword;

    public LoginViewModel(IApiService api, IAuthStateService authState)
    {
        _api = api;
        _authState = authState;
        Title = "Sign In";
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Please enter your email and password.";
            return;
        }
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.LoginAsync(new LoginRequest { Email = Email, Password = Password });
            if (result?.Success == true && result.Data != null)
            {
                await _authState.SetAuthAsync(result.Data.AccessToken, result.Data.RefreshToken, result.Data.User);
                if (Shell.Current is global::ShopForge.Mobile.AppShell appShell)
                {
                    appShell.UpdateAuthState(true);
                }

                await Shell.Current.GoToAsync("//home");
            }
            else
            {
                ErrorMessage = result?.Message ?? "Login failed. Please check your credentials.";
            }
        });
    }

    [RelayCommand]
    private async Task GoToRegisterAsync() => await Shell.Current.GoToAsync("register");

    [RelayCommand]
    private async Task GoToForgotPasswordAsync() => await Shell.Current.GoToAsync("forgot-password");

    [RelayCommand]
    private void TogglePasswordVisibility() => ShowPassword = !ShowPassword;
}
