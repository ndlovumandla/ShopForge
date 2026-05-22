using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Checkout;

public partial class CheckoutAddressPage : ContentPage
{
    private readonly CheckoutAddressViewModel _vm;

    public CheckoutAddressPage(CheckoutAddressViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _vm.LoadAddressesAsync();
    }
}