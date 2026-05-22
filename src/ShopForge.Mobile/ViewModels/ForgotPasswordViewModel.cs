using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;

namespace ShopForge.Mobile.ViewModels;

public partial class ForgotPasswordViewModel : BaseViewModel
{
    private readonly IApiService _api;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private bool _emailSent;

    public ForgotPasswordViewModel(IApiService api) { _api = api; Title = "Forgot Password"; }

    [RelayCommand]
    private async Task SubmitAsync()
    {
        if (string.IsNullOrWhiteSpace(Email)) { ErrorMessage = "Please enter your email."; return; }
        await ExecuteSafelyAsync(async () => { await _api.ForgotPasswordAsync(Email); EmailSent = true; });
    }
}
