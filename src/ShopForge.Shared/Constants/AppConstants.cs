namespace ShopForge.Shared.Constants;

public static class AppConstants
{
    public const int DefaultPageSize = 20;
    public const int MaxPageSize = 100;

    public static class CacheKeys
    {
        public const string Products = "products";
        public const string Categories = "categories";
        public const string FeaturedProducts = "featured_products";
        public const string NewArrivals = "new_arrivals";
        public const string Banners = "banners";
        public const string ShippingMethods = "shipping_methods";
    }

    public static class JwtClaims
    {
        public const string UserId = "uid";
        public const string Email = "email";
        public const string Role = "role";
        public const string FirstName = "firstName";
        public const string LastName = "lastName";
    }

    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Customer = "Customer";
        public const string AdminOrManager = "Admin,Manager";
    }

    public static class Idempotency
    {
        public const string HeaderName = "Idempotency-Key";
        public const int MaxKeyLength = 128;
        public const int CacheMinutes = 10;
    }

    public static class PaymentOutcomes
    {
        public const string AlwaysSucceedSuffix = "0001";
        public const string DeclineInsufficientFunds = "0002";
        public const string DeclineCardExpired = "0003";
        public const string DeclineFraudSuspected = "0004";
    }
}
