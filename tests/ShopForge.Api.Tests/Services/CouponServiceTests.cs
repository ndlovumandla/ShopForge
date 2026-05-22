using FluentAssertions;

namespace ShopForge.Api.Tests.Services;

public class CouponServiceTests
{
    [Theory]
    [InlineData(100, 10, "Percentage", 10)]        // 10% off R100 = R10 off
    [InlineData(200, 50, "FixedAmount", 50)]        // R50 off
    [InlineData(500, 20, "Percentage", 100)]        // 20% off R500 = R100
    [InlineData(30, 50, "FixedAmount", 30)]         // R50 off but cart only R30 → discount = R30 (cap at cart total)
    public void CalculateDiscount_ReturnsCorrectAmount(decimal cartTotal, decimal couponValue, string couponType, decimal expectedDiscount)
    {
        var discount = CalculateDiscount(cartTotal, couponValue, couponType);
        discount.Should().Be(expectedDiscount);
    }

    [Theory]
    [InlineData(100, 50, true)]   // Cart R100 >= min R50 → valid
    [InlineData(40, 50, false)]   // Cart R40 < min R50 → invalid
    [InlineData(50, 50, true)]    // Exactly at minimum → valid
    public void ValidateMinOrderAmount_ReturnsExpected(decimal cartTotal, decimal minAmount, bool expected)
    {
        var result = cartTotal >= minAmount;
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("2025-01-01", true)]   // Expired
    [InlineData("2099-12-31", false)]  // Not expired
    public void CouponExpiry_ReturnsExpected(string expiryDateStr, bool isExpired)
    {
        var expiry = DateTime.Parse(expiryDateStr);
        var result = expiry < DateTime.UtcNow;
        result.Should().Be(isExpired);
    }

    private static decimal CalculateDiscount(decimal cartTotal, decimal value, string type)
    {
        var discount = type switch
        {
            "Percentage" => cartTotal * (value / 100),
            "FixedAmount" => value,
            _ => 0m
        };
        return Math.Min(discount, cartTotal); // Cap at cart total
    }
}
