using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Auth;

namespace ShopForge.Mobile.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    private readonly IApiService _api;
    private readonly IAuthStateService _authState;

    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string _confirmPassword = string.Empty;

    public RegisterViewModel(IApiService api, IAuthStateService authState)
    {
        _api = api;
        _authState = authState;
        Title = "Create Account";
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) ||
            string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "All fields are required.";
            return;
        }
        if (Password != ConfirmPassword) { ErrorMessage = "Passwords do not match."; return; }
        if (Password.Length < 8) { ErrorMessage = "Password must be at least 8 characters."; return; }

        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.RegisterAsync(new RegisterRequest { FirstName = FirstName, LastName = LastName, Email = Email, Password = Password });
            if (result?.Success == true && result.Data != null)
            {
                await _authState.SetAuthAsync(result.Data.AccessToken, result.Data.RefreshToken, result.Data.User);
                if (Shell.Current is global::ShopForge.Mobile.AppShell appShell)
                {
                    appShell.UpdateAuthState(true);
                }

                await Shell.Current.GoToAsync("//home");
            }
            else { ErrorMessage = result?.Message ?? "Registration failed."; }
        });
    }

    [RelayCommand]
    private async Task GoToLoginAsync() => await Shell.Current.GoToAsync("login");
}
