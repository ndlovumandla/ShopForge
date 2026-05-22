namespace ShopForge.Mobile.Services;

public class CartLocalService : ICartLocalService
{
    private int _count;
    public event EventHandler<int>? CartCountChanged;

    public CartLocalService()
    {
        _count = Preferences.Get("cart_count", 0);
    }

    public int GetCartItemCount() => _count;

    public void SetCartItemCount(int count)
    {
        _count = count;
        Preferences.Set("cart_count", count);
        CartCountChanged?.Invoke(this, count);
    }
}
