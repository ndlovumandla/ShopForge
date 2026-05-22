using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Account;

public partial class AccountPage : ContentPage
{
    private readonly AccountViewModel _vm;

    public AccountPage(AccountViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _vm.LoadProfileAsync();
    }
}