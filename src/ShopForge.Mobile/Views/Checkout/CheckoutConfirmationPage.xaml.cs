using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Checkout;

public partial class CheckoutConfirmationPage : ContentPage
{
    public CheckoutConfirmationPage(CheckoutConfirmationViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}