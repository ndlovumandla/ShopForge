using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ShopForge.Mobile.Services;
using ShopForge.Mobile.ViewModels;
using ShopForge.Mobile.Views.Auth;
using ShopForge.Mobile.Views.Home;
using ShopForge.Mobile.Views.Shop;
using ShopForge.Mobile.Views.Cart;
using ShopForge.Mobile.Views.Checkout;
using ShopForge.Mobile.Views.Orders;
using ShopForge.Mobile.Views.Wishlist;
using ShopForge.Mobile.Views.Account;

namespace ShopForge.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddHttpClient<IApiService, ApiService>(client =>
        {
            var defaultBaseUrl = GetDefaultApiBaseUrl();
            var baseUrl = Preferences.Get("api_base_url", defaultBaseUrl);
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        builder.Services.AddSingleton<IAuthStateService, AuthStateService>();
        builder.Services.AddSingleton<ICartLocalService, CartLocalService>();

        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<ForgotPasswordViewModel>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<ShopViewModel>();
        builder.Services.AddTransient<ProductDetailViewModel>();
        builder.Services.AddTransient<CartViewModel>();
        builder.Services.AddTransient<CheckoutAddressViewModel>();
        builder.Services.AddTransient<CheckoutShippingViewModel>();
        builder.Services.AddTransient<CheckoutPaymentViewModel>();
        builder.Services.AddTransient<CheckoutConfirmationViewModel>();
        builder.Services.AddTransient<OrdersViewModel>();
        builder.Services.AddTransient<OrderDetailViewModel>();
        builder.Services.AddTransient<WishlistViewModel>();
        builder.Services.AddTransient<AccountViewModel>();
        builder.Services.AddTransient<ProfileEditViewModel>();
        builder.Services.AddTransient<AddressListViewModel>();
        builder.Services.AddTransient<AddressFormViewModel>();
        builder.Services.AddTransient<NotificationsViewModel>();
        builder.Services.AddTransient<WriteReviewViewModel>();
        builder.Services.AddTransient<SearchViewModel>();
        builder.Services.AddTransient<ChangePasswordViewModel>();

        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<ForgotPasswordPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<ShopPage>();
        builder.Services.AddTransient<ProductDetailPage>();
        builder.Services.AddTransient<SearchPage>();
        builder.Services.AddTransient<CartPage>();
        builder.Services.AddTransient<CheckoutAddressPage>();
        builder.Services.AddTransient<CheckoutShippingPage>();
        builder.Services.AddTransient<CheckoutPaymentPage>();
        builder.Services.AddTransient<CheckoutConfirmationPage>();
        builder.Services.AddTransient<OrdersPage>();
        builder.Services.AddTransient<OrderDetailPage>();
        builder.Services.AddTransient<WishlistPage>();
        builder.Services.AddTransient<AccountPage>();
        builder.Services.AddTransient<ProfileEditPage>();
        builder.Services.AddTransient<AddressListPage>();
        builder.Services.AddTransient<AddressFormPage>();
        builder.Services.AddTransient<NotificationsPage>();
        builder.Services.AddTransient<WriteReviewPage>();
        builder.Services.AddTransient<ChangePasswordPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }

    private static string GetDefaultApiBaseUrl()
    {
#if ANDROID
        return "http://10.0.2.2:5002";
#else
        return "http://127.0.0.1:5002";
#endif
    }
}