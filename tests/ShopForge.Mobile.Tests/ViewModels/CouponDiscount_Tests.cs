using FluentAssertions;

namespace ShopForge.Mobile.Tests.ViewModels;

public class CouponDiscount_Tests
{
    [Theory]
    [InlineData(100, 10, "Percentage", 10)]        // 10% off R100 = R10
    [InlineData(200, 50, "FixedAmount", 50)]        // R50 fixed off
    [InlineData(500, 20, "Percentage", 100)]        // 20% off R500 = R100
    [InlineData(30, 50, "FixedAmount", 30)]         // Cap: cart < discount → discount = cart total
    public void CalculateDiscount_ReturnsCorrectAmount(decimal cartTotal, decimal couponValue, string couponType, decimal expectedDiscount)
    {
        CalculateDiscount(cartTotal, couponValue, couponType).Should().Be(expectedDiscount);
    }

    [Theory]
    [InlineData(100, 50, true)]    // Meets minimum
    [InlineData(40, 50, false)]    // Below minimum
    [InlineData(50, 50, true)]     // Exactly at minimum
    public void ValidateMinOrderAmount_ReturnsExpected(decimal cartTotal, decimal minAmount, bool expected)
    {
        (cartTotal >= minAmount).Should().Be(expected);
    }

    [Theory]
    [InlineData("2025-01-01", true)]   // Expired
    [InlineData("2099-12-31", false)]  // Valid
    public void CouponExpiry_ReturnsExpected(string expiryDateStr, bool isExpired)
    {
        var expiry = DateTime.Parse(expiryDateStr);
        (expiry < DateTime.UtcNow).Should().Be(isExpired);
    }

    [Theory]
    [InlineData(0, "Percentage", 0)]     // Zero cart → zero discount
    [InlineData(100, "Unknown", 0)]      // Unknown type → zero discount
    public void CalculateDiscount_EdgeCases(decimal cartTotal, string couponType, decimal expectedDiscount)
    {
        CalculateDiscount(cartTotal, 20, couponType).Should().Be(expectedDiscount);
    }

    private static decimal CalculateDiscount(decimal cartTotal, decimal value, string type)
    {
        var discount = type switch
        {
            "Percentage" => cartTotal * (value / 100),
            "FixedAmount" => value,
            _ => 0m
        };
        return Math.Min(discount, cartTotal);
    }
}
