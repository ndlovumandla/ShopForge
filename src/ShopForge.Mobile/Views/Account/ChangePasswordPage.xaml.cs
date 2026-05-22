using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Account;

public partial class ChangePasswordPage : ContentPage
{
    public ChangePasswordPage(ChangePasswordViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
