using System.Globalization;
using System.Collections;

namespace ShopForge.Mobile.Converters;

public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            bool b => b,
            int i => i > 0,
            long l => l > 0,
            ICollection collection => collection.Count > 0,
            _ => false
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => value is bool b && b;
}

public class InvertedBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => value is bool b && !b;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => value is bool b && !b;
}

public class NullToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value != null && !string.IsNullOrEmpty(value.ToString());
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => null;
}

public class StringToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() switch
        {
            "Pending" => Colors.Orange,
            "Confirmed" => Colors.Blue,
            "Processing" => Colors.Purple,
            "Shipped" => Colors.Teal,
            "Delivered" => Colors.Green,
            "Cancelled" => Colors.Red,
            "Refunded" => Colors.Gray,
            _ => Colors.Gray
        };
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => null;
}
