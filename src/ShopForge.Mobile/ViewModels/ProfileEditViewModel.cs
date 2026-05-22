using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;

namespace ShopForge.Mobile.ViewModels;

public partial class ProfileEditViewModel : BaseViewModel
{
    private readonly IApiService _api;
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string? _phoneNumber;

    public ProfileEditViewModel(IApiService api) { _api = api; Title = "Edit Profile"; }

    [RelayCommand]
    public async Task LoadAsync()
    {
        var result = await _api.GetProfileAsync();
        if (result?.Data != null) { FirstName = result.Data.FirstName; LastName = result.Data.LastName; PhoneNumber = result.Data.PhoneNumber; }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.UpdateProfileAsync(new { firstName = FirstName, lastName = LastName, phoneNumber = PhoneNumber });
            if (result?.Success == true) await Shell.Current.GoToAsync("..");
            else ErrorMessage = result?.Message ?? "Update failed.";
        });
    }
}
