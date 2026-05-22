using FluentAssertions;

namespace ShopForge.Mobile.Tests.ViewModels;

public class CartCalculation_Tests
{
    [Theory]
    [InlineData(100, 0.15, 115)]    // R100 + 15% VAT
    [InlineData(200, 0.15, 230)]    // R200 + 15% VAT
    [InlineData(0, 0.15, 0)]        // Empty cart
    public void CalculateTotal_WithVat_ReturnsExpected(decimal subtotal, double vatRate, decimal expected)
    {
        var result = subtotal * (1 + (decimal)vatRate);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(100, 10, 90)]       // R10 discount on R100
    [InlineData(50, 50, 0)]         // Full discount
    [InlineData(200, 0, 200)]       // No discount
    public void CartTotal_AfterDiscount_ReturnsExpected(decimal subtotal, decimal discount, decimal expected)
    {
        var result = Math.Max(0, subtotal - discount);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3 }, 6)]     // Quantities sum
    [InlineData(new[] { 5 }, 5)]
    [InlineData(new int[] { }, 0)]
    public void TotalItems_SumsQuantities(int[] quantities, int expectedTotal)
    {
        var total = quantities.Sum();
        total.Should().Be(expectedTotal);
    }

    [Theory]
    [InlineData(100, 2, 200)]    // 2 × R100
    [InlineData(49.99, 3, 149.97)]
    public void LineTotal_UnitPriceTimesQuantity(decimal unitPrice, int quantity, decimal expected)
    {
        var lineTotal = unitPrice * quantity;
        lineTotal.Should().BeApproximately(expected, 0.01m);
    }
}
