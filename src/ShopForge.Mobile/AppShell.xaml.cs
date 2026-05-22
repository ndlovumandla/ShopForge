using ShopForge.Mobile.Services;
using ShopForge.Mobile.Views.Auth;
using ShopForge.Mobile.Views.Shop;
using ShopForge.Mobile.Views.Checkout;
using ShopForge.Mobile.Views.Orders;
using ShopForge.Mobile.Views.Account;
using System.Diagnostics;

namespace ShopForge.Mobile;

public partial class AppShell : Shell
{
    private readonly IAuthStateService _authState;
    private readonly IApiService _api;
    private bool _initialRouteSet;

    public AppShell(IAuthStateService authState, IApiService api)
    {
        _authState = authState;
        _api = api;
        InitializeComponent();
        RegisterRoutes();
        Loaded += OnShellLoaded;
    }

    private void RegisterRoutes()
    {
        RegisterRoute<LoginPage>("login");
        RegisterRoute<RegisterPage>("register");
        RegisterRoute<ForgotPasswordPage>("forgot-password");
        RegisterRoute<Views.Shop.ProductDetailPage>("product-detail");
        RegisterRoute<Views.Shop.SearchPage>("search");
        RegisterRoute<CheckoutAddressPage>("checkout/address");
        RegisterRoute<CheckoutShippingPage>("checkout/shipping");
        RegisterRoute<CheckoutPaymentPage>("checkout/payment");
        RegisterRoute<CheckoutConfirmationPage>("checkout/confirmation");
        RegisterRoute<OrderDetailPage>("order-detail");
        RegisterRoute<Views.Account.ProfileEditPage>("profile-edit");
        RegisterRoute<Views.Account.AddressListPage>("address-list");
        RegisterRoute<Views.Account.AddressFormPage>("address-form");
        RegisterRoute<Views.Account.NotificationsPage>("notifications");
        RegisterRoute<Views.Account.ChangePasswordPage>("change-password");
        RegisterRoute<Views.Account.WriteReviewPage>("write-review");
    }

    private static void RegisterRoute<TPage>(string route) where TPage : Element
    {
        Routing.RegisterRoute(route, new DiRouteFactory(typeof(TPage)));
    }

    private async void OnShellLoaded(object? sender, EventArgs e)
    {
        if (_initialRouteSet)
            return;

        _initialRouteSet = true;
        try
        {
            await SetInitialRouteAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"SetInitialRouteAsync failed: {ex}");
            await _authState.ClearAuthAsync();
        }
    }

    private async Task SetInitialRouteAsync()
    {
        UpdateAuthState(false);

        // Always start on login to avoid showing authenticated tabs before token validation completes.
        await GoToAsync("login");

        if (!_authState.IsAuthenticated)
            return;

        var me = await _api.GetProfileAsync();
        if (me?.Success == true && me.Data != null)
        {
            UpdateAuthState(true);
            CurrentItem = ShellTabs;
            return;
        }

        await _authState.ClearAuthAsync();
    }

    public void UpdateAuthState(bool isAuthenticated)
    {
        if (isAuthenticated)
        {
            CurrentItem = ShellTabs;
        }
    }
}