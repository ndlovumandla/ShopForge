using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Checkout;

public partial class CheckoutShippingPage : ContentPage
{
    private readonly CheckoutShippingViewModel _vm;

    public CheckoutShippingPage(CheckoutShippingViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _vm.LoadShippingMethodsAsync();
    }
}