using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Shop;

public partial class ProductDetailPage : ContentPage
{
    public ProductDetailPage(ProductDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}