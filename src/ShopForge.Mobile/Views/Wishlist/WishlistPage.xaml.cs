using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Wishlist;

public partial class WishlistPage : ContentPage
{
    private readonly WishlistViewModel _vm;

    public WishlistPage(WishlistViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _vm.LoadWishlistAsync();
    }
}