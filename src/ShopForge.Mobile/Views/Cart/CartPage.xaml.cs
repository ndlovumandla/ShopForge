using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Cart;

public partial class CartPage : ContentPage
{
    private readonly CartViewModel _vm;

    public CartPage(CartViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _vm.LoadCartAsync();
    }
}