using ShopForge.Mobile.Services;
namespace ShopForge.Mobile;

public partial class App : Application
{
    private readonly IAuthStateService _authState;
    private readonly IApiService _api;

    public App(IAuthStateService authState, IApiService api)
    {
        InitializeComponent();
        _authState = authState;
        _api = api;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell(_authState, _api));
    }
}