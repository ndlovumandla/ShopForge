using Microsoft.Maui.Controls;

namespace ShopForge.Mobile.Services;

internal sealed class DiRouteFactory : RouteFactory
{
    private readonly Type _pageType;

    public DiRouteFactory(Type pageType)
    {
        _pageType = pageType;
    }

    public override Element GetOrCreate()
    {
        return (Element)Activator.CreateInstance(_pageType)!;
    }

    public override Element GetOrCreate(IServiceProvider services)
    {
        var resolved = services.GetService(_pageType) ?? Activator.CreateInstance(_pageType);
        return (Element)resolved!;
    }

    public override bool Equals(object? obj)
    {
        return obj is DiRouteFactory other && other._pageType == _pageType;
    }

    public override int GetHashCode()
    {
        return _pageType.GetHashCode();
    }
}