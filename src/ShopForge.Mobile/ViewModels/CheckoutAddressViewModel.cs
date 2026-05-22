using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Mobile.ViewModels;

public partial class CheckoutAddressViewModel : BaseViewModel
{
    private readonly IApiService _api;
    [ObservableProperty] private ObservableCollection<AddressDto> _addresses = new();
    [ObservableProperty] private AddressDto? _selectedAddress;

    public CheckoutAddressViewModel(IApiService api) { _api = api; Title = "Select Address"; }

    [RelayCommand]
    public async Task LoadAddressesAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.GetAddressesAsync();
            if (result?.Data != null)
            {
                Addresses.Clear();
                foreach (var a in result.Data) Addresses.Add(a);
                SelectedAddress = result.Data.FirstOrDefault(a => a.IsDefault) ?? result.Data.FirstOrDefault();
            }
        });
    }

    [RelayCommand]
    private async Task ContinueAsync()
    {
        if (SelectedAddress == null) { ErrorMessage = "Please select a delivery address."; return; }
        await Shell.Current.GoToAsync($"checkout/shipping?addressId={SelectedAddress.Id}");
    }

    [RelayCommand]
    private async Task AddNewAddressAsync() => await Shell.Current.GoToAsync("address-form");
}
