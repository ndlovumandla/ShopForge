using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Account;

public partial class ProfileEditPage : ContentPage
{
    private readonly ProfileEditViewModel _vm;

    public ProfileEditPage(ProfileEditViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _vm.LoadAsync();
    }
}