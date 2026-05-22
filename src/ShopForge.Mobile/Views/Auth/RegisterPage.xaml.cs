using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Auth;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}