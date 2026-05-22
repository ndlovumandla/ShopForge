using ShopForge.Mobile.ViewModels;

namespace ShopForge.Mobile.Views.Account;

public partial class WriteReviewPage : ContentPage
{
    public WriteReviewPage(WriteReviewViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}