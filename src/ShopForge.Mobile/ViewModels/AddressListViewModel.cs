using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Mobile.ViewModels;

public partial class AddressListViewModel : BaseViewModel
{
    private readonly IApiService _api;
    [ObservableProperty] private ObservableCollection<AddressDto> _addresses = new();

    public AddressListViewModel(IApiService api) { _api = api; Title = "My Addresses"; }

    [RelayCommand]
    public async Task LoadAsync()
    {
        var result = await _api.GetAddressesAsync();
        if (result?.Data != null) { Addresses.Clear(); foreach (var a in result.Data) Addresses.Add(a); }
    }

    [RelayCommand]
    private async Task AddAsync() => await Shell.Current.GoToAsync("address-form");

    [RelayCommand]
    private async Task EditAsync(AddressDto address) => await Shell.Current.GoToAsync($"address-form?id={address.Id}");

    [RelayCommand]
    private async Task DeleteAsync(AddressDto address)
    {
        bool ok = await Shell.Current.DisplayAlertAsync("Delete", "Delete this address?", "Yes", "No");
        if (!ok) return;
        await ExecuteSafelyAsync(async () => { await _api.DeleteAddressAsync(address.Id); Addresses.Remove(address); });
    }

    [RelayCommand]
    private async Task SetDefaultAsync(AddressDto address)
    {
        await ExecuteSafelyAsync(async () => { await _api.SetDefaultAddressAsync(address.Id); await LoadAsync(); });
    }
}
