using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Mobile.ViewModels;

[QueryProperty(nameof(AddressId), "id")]
public partial class AddressFormViewModel : BaseViewModel
{
    private readonly IApiService _api;
    [ObservableProperty] private int _addressId;
    [ObservableProperty] private string _label = string.Empty;
    [ObservableProperty] private string _fullName = string.Empty;
    [ObservableProperty] private string? _phoneNumber;
    [ObservableProperty] private string _line1 = string.Empty;
    [ObservableProperty] private string? _line2;
    [ObservableProperty] private string _city = string.Empty;
    [ObservableProperty] private string _state = string.Empty;
    [ObservableProperty] private string _postalCode = string.Empty;
    [ObservableProperty] private string _country = "ZA";

    public bool IsEdit => AddressId > 0;

    public AddressFormViewModel(IApiService api) { _api = api; Title = "Add Address"; }

    partial void OnAddressIdChanged(int value) { if (value > 0) { Title = "Edit Address"; _ = LoadAsync(); } }

    [RelayCommand]
    private async Task LoadAsync()
    {
        var result = await _api.GetAddressesAsync();
        var addr = result?.Data?.FirstOrDefault(a => a.Id == AddressId);
        if (addr != null) { Label = addr.Label ?? ""; FullName = addr.FullName; PhoneNumber = addr.PhoneNumber; Line1 = addr.Line1; Line2 = addr.Line2; City = addr.City; State = addr.State; PostalCode = addr.PostalCode; Country = addr.Country; }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Line1) || string.IsNullOrWhiteSpace(City))
        {
            ErrorMessage = "Full name, address and city are required."; return;
        }
        await ExecuteSafelyAsync(async () =>
        {
            var dto = new AddressDto { Id = AddressId, Label = Label, FullName = FullName, PhoneNumber = PhoneNumber, Line1 = Line1, Line2 = Line2, City = City, State = State, PostalCode = PostalCode, Country = Country };
            if (IsEdit) await _api.UpdateAddressAsync(AddressId, dto);
            else await _api.CreateAddressAsync(dto);
            await Shell.Current.GoToAsync("..");
        });
    }
}
