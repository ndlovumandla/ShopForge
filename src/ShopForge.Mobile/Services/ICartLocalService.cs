namespace ShopForge.Mobile.Services;

public interface ICartLocalService
{
    int GetCartItemCount();
    void SetCartItemCount(int count);
    event EventHandler<int> CartCountChanged;
}
