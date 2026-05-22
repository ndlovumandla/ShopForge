using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Admin;

namespace ShopForge.Mobile.ViewModels;

[QueryProperty(nameof(AddressId), "addressId")]
public partial class CheckoutShippingViewModel : BaseViewModel
{
    private readonly IApiService _api;
    [ObservableProperty] private int _addressId;
    [ObservableProperty] private ObservableCollection<ShippingMethodDto> _shippingMethods = new();
    [ObservableProperty] private ShippingMethodDto? _selectedMethod;

    public CheckoutShippingViewModel(IApiService api) { _api = api; Title = "Shipping Method"; }

    [RelayCommand]
    public async Task LoadShippingMethodsAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.GetShippingMethodsAsync();
            if (result?.Data != null)
            {
                ShippingMethods.Clear();
                foreach (var s in result.Data.Where(s => s.IsActive)) ShippingMethods.Add(s);
                SelectedMethod = ShippingMethods.FirstOrDefault();

                if (ShippingMethods.Count == 0)
                    ErrorMessage = "No shipping methods are available right now.";

                return;
            }

            ErrorMessage = "Unable to load shipping methods. Please try again.";
        });
    }

    [RelayCommand]
    private async Task ContinueAsync()
    {
        if (SelectedMethod == null) { ErrorMessage = "Please select a shipping method."; return; }
        await Shell.Current.GoToAsync($"checkout/payment?addressId={AddressId}&shippingMethodId={SelectedMethod.Id}&shippingCost={SelectedMethod.Cost}");
    }
}
