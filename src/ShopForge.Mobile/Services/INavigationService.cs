namespace ShopForge.Mobile.Services;

public interface INavigationService
{
    Task NavigateToAsync(string route);
    Task GoBackAsync();
}
