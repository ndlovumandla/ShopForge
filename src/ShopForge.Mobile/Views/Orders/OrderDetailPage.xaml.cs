using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Orders;

public partial class OrderDetailPage : ContentPage
{
    public OrderDetailPage(OrderDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}