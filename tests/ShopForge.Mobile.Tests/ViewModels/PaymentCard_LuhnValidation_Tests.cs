using FluentAssertions;

namespace ShopForge.Mobile.Tests.ViewModels;

public class PaymentCard_LuhnValidation_Tests
{
    [Theory]
    [InlineData("4111111111111111", true)]   // Valid Visa
    [InlineData("5500005555555559", true)]   // Valid Mastercard
    [InlineData("378282246310005", true)]    // Valid Amex
    [InlineData("4111111111111112", false)]  // Invalid check digit
    [InlineData("1234567890123456", false)]  // Fails Luhn
    [InlineData("", false)]                  // Empty
    [InlineData("123", false)]               // Too short
    public void LuhnCheck_ReturnsExpected(string cardNumber, bool expected)
    {
        LuhnValidate(cardNumber).Should().Be(expected);
    }

    [Theory]
    [InlineData("4111111111110001", "Visa")]
    [InlineData("5500005555555559", "Mastercard")]
    [InlineData("378282246310005", "Amex")]
    [InlineData("6011111111111117", "Discover")]
    [InlineData("9999999999999999", "Unknown")]
    public void DetectCardBrand_ReturnsCorrectBrand(string cardNumber, string expectedBrand)
    {
        DetectBrand(cardNumber).Should().Be(expectedBrand);
    }

    [Theory]
    [InlineData("4111111111110002", false, "Insufficient funds")]
    [InlineData("4111111111110003", false, "Card expired")]
    [InlineData("4111111111110004", false, "Card declined")]
    [InlineData("4111111111110001", true, null)]
    public void MockPaymentOutcome_BasedOnLastFourDigits(string cardNumber, bool shouldSucceed, string? failureReason)
    {
        var last4 = cardNumber.Length >= 4 ? cardNumber[^4..] : cardNumber;
        var (success, reason) = GetMockOutcome(last4);
        success.Should().Be(shouldSucceed);
        if (failureReason != null)
            reason.Should().Contain(failureReason);
    }

    private static bool LuhnValidate(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber)) return false;
        var digits = cardNumber.Where(char.IsDigit).ToArray();
        if (digits.Length < 13 || digits.Length > 19) return false;
        int sum = 0;
        bool doubleUp = false;
        for (int i = digits.Length - 1; i >= 0; i--)
        {
            int d = digits[i] - '0';
            if (doubleUp) { d *= 2; if (d > 9) d -= 9; }
            sum += d;
            doubleUp = !doubleUp;
        }
        return sum % 10 == 0;
    }

    private static string DetectBrand(string cardNumber)
    {
        var digits = new string(cardNumber.Where(char.IsDigit).ToArray());
        if (digits.StartsWith("4")) return "Visa";
        if (digits.Length >= 2)
        {
            var first2 = int.Parse(digits[..2]);
            if (first2 >= 51 && first2 <= 55) return "Mastercard";
        }
        if (digits.StartsWith("34") || digits.StartsWith("37")) return "Amex";
        if (digits.StartsWith("6011")) return "Discover";
        return "Unknown";
    }

    private static (bool success, string? reason) GetMockOutcome(string last4) => last4 switch
    {
        "0002" => (false, "Insufficient funds"),
        "0003" => (false, "Card expired"),
        "0004" => (false, "Card declined"),
        _ => (true, null)
    };
}
