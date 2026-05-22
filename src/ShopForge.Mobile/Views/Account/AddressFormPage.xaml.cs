using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Account;

public partial class AddressFormPage : ContentPage
{
    public AddressFormPage(AddressFormViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}