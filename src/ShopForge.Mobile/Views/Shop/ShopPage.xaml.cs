using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Shop;

public partial class ShopPage : ContentPage
{
    private readonly ShopViewModel _vm;

    public ShopPage(ShopViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_vm.Products.Count == 0)
            _vm.LoadProductsCommand.Execute(true);
    }
}