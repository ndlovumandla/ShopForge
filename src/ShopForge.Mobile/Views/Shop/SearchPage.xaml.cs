using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Shop;

public partial class SearchPage : ContentPage
{
    public SearchPage(SearchViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}