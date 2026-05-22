using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Checkout;

public partial class CheckoutPaymentPage : ContentPage
{
    public CheckoutPaymentPage(CheckoutPaymentViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}