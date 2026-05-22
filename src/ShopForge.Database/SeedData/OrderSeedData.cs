using Microsoft.EntityFrameworkCore;
using ShopForge.Database.Entities;

namespace ShopForge.Database.SeedData;

public static class OrderSeedData
{
    private static readonly DateTime ReportingWindowStart = new(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly DateTime ReportingWindowEnd = new(2026, 5, 22, 0, 0, 0, DateTimeKind.Utc);

    // Matches product data in ProductSeedData — index = ProductId - 1
    private static readonly (string Name, string Sku, decimal Price)[] ProductRef =
    {
        ("4K Smart TV 55\"",              "ELEC-001", 15999m), // 0  → ProdId 1
        ("Laptop Pro 15\"",               "ELEC-002", 22999m), // 1  → ProdId 2
        ("Wireless Headphones",           "ELEC-003",  2499m), // 2  → ProdId 3
        ("Smartphone X12",                "ELEC-004", 12999m), // 3  → ProdId 4
        ("Bluetooth Speaker",             "ELEC-005",  1299m), // 4  → ProdId 5
        ("Gaming Console",                "ELEC-006",  9999m), // 5  → ProdId 6
        ("Tablet 10\"",                   "ELEC-007",  6999m), // 6  → ProdId 7
        ("Smartwatch Series 5",           "ELEC-008",  3999m), // 7  → ProdId 8
        ("Wireless Earbuds",              "ELEC-009",  1999m), // 8  → ProdId 9
        ("Digital Camera 24MP",           "ELEC-010",  8999m), // 9  → ProdId 10
        ("USB-C Hub 7-in-1",              "ELEC-011",   599m), // 10 → ProdId 11
        ("Mechanical Keyboard",           "ELEC-012",  1499m), // 11 → ProdId 12
        ("Gaming Mouse",                  "ELEC-013",   899m), // 12 → ProdId 13
        ("27\" Monitor",                  "ELEC-014",  5999m), // 13 → ProdId 14
        ("Portable Charger 20000mAh",     "ELEC-015",   499m), // 14 → ProdId 15
        ("Wi-Fi Router AC3000",           "ELEC-016",  1899m), // 15 → ProdId 16
        ("NAS Drive 4TB",                 "ELEC-017",  4999m), // 16 → ProdId 17
        ("VR Headset",                    "ELEC-018",  7999m), // 17 → ProdId 18
        ("Drone with Camera",             "ELEC-019", 11999m), // 18 → ProdId 19
        ("Smart Home Hub",                "ELEC-020",  1299m), // 19 → ProdId 20
        ("Men's Slim Fit Jeans",          "CLTH-001",   699m), // 20 → ProdId 21
        ("Women's Summer Dress",          "CLTH-002",   599m), // 21 → ProdId 22
        ("Unisex Hoodie",                 "CLTH-003",   499m), // 22 → ProdId 23
        ("Men's Business Shirt",          "CLTH-004",   449m), // 23 → ProdId 24
        ("Women's Yoga Leggings",         "CLTH-005",   399m), // 24 → ProdId 25
        ("Men's Polo Shirt",              "CLTH-006",   349m), // 25 → ProdId 26
        ("Women's Blazer",                "CLTH-007",   899m), // 26 → ProdId 27
        ("Men's Chino Pants",             "CLTH-008",   599m), // 27 → ProdId 28
        ("Women's Denim Jacket",          "CLTH-009",   799m), // 28 → ProdId 29
        ("Men's Cargo Shorts",            "CLTH-010",   449m), // 29 → ProdId 30
        ("Women's Cardigan",              "CLTH-011",   549m), // 30 → ProdId 31
        ("Men's V-Neck Sweater",          "CLTH-012",   499m), // 31 → ProdId 32
        ("Women's Mini Skirt",            "CLTH-013",   349m), // 32 → ProdId 33
        ("Men's Tracksuit",               "CLTH-014",   799m), // 33 → ProdId 34
        ("Women's Windbreaker",           "CLTH-015",   699m), // 34 → ProdId 35
        ("Coffee Table Oak",              "HOME-001",  2999m), // 35 → ProdId 36
        ("Queen Bed Frame",               "HOME-002",  4999m), // 36 → ProdId 37
        ("Floor Lamp Modern",             "HOME-003",   799m), // 37 → ProdId 38
        ("Wall Clock Minimalist",         "HOME-004",   349m), // 38 → ProdId 39
        ("Throw Pillow Set 4pc",          "HOME-005",   299m), // 39 → ProdId 40
        ("Kitchen Knife Set 7pc",         "HOME-006",   999m), // 40 → ProdId 41
        ("Non-Stick Cookware Set",        "HOME-007",  1799m), // 41 → ProdId 42
        ("Garden Hose 30m",               "HOME-008",   399m), // 42 → ProdId 43
        ("Ceramic Indoor Plant Pot",      "HOME-009",   199m), // 43 → ProdId 44
        ("Blackout Curtain Set",          "HOME-010",   599m), // 44 → ProdId 45
        ("Bath Towel Set 6pc",            "HOME-011",   449m), // 45 → ProdId 46
        ("Waterproof Shower Curtain",     "HOME-012",   249m), // 46 → ProdId 47
        ("Bamboo Desk Organizer",         "HOME-013",   299m), // 47 → ProdId 48
        ("Bookshelf 5-Tier",              "HOME-014",  1499m), // 48 → ProdId 49
        ("Non-Slip Doormat",              "HOME-015",   149m), // 49 → ProdId 50
        ("Premium Yoga Mat",              "SPRT-001",   399m), // 50 → ProdId 51
        ("Adjustable Dumbbell Set 20kg",  "SPRT-002",  1999m), // 51 → ProdId 52
        ("Trail Running Shoes",           "SPRT-003",  1299m), // 52 → ProdId 53
        ("Sports Gym Bag 40L",            "SPRT-004",   549m), // 53 → ProdId 54
        ("Resistance Bands Set 5pc",      "SPRT-005",   249m), // 54 → ProdId 55
        ("Speed Jump Rope",               "SPRT-006",   149m), // 55 → ProdId 56
        ("Cycling Safety Helmet",         "SPRT-007",   799m), // 56 → ProdId 57
        ("Insulated Water Bottle 1L",     "SPRT-008",   299m), // 57 → ProdId 58
        ("Tennis Racket Pro",             "SPRT-009",   899m), // 58 → ProdId 59
        ("Match Football Size 5",         "SPRT-010",   349m), // 59 → ProdId 60
        ("The Art of Clean Coding",       "BOOK-001",   349m), // 60 → ProdId 61
        ("Business Strategy 101",         "BOOK-002",   299m), // 61 → ProdId 62
        ("Mindfulness for Beginners",     "BOOK-003",   249m), // 62 → ProdId 63
        ("The Cooking Masterclass",       "BOOK-004",   449m), // 63 → ProdId 64
        ("World History Encyclopedia",    "BOOK-005",   699m), // 64 → ProdId 65
        ("Science Fiction Anthology",     "BOOK-006",   199m), // 65 → ProdId 66
        ("Children's Adventure Series",   "BOOK-007",   249m), // 66 → ProdId 67
        ("Photography Basics Guide",      "BOOK-008",   349m), // 67 → ProdId 68
        ("Financial Freedom Guide",       "BOOK-009",   299m), // 68 → ProdId 69
        ("Language Learning Pack",        "BOOK-010",   549m), // 69 → ProdId 70
        ("Daily Multivitamin Complex",    "HLTH-001",   299m), // 70 → ProdId 71
        ("Whey Protein Powder 1kg",       "HLTH-002",   699m), // 71 → ProdId 72
        ("Face Moisturizer SPF30",        "HLTH-003",   349m), // 72 → ProdId 73
        ("Hair Growth Serum",             "HLTH-004",   449m), // 73 → ProdId 74
        ("Sonic Electric Toothbrush",     "HLTH-005",   799m), // 74 → ProdId 75
        ("Digital Infrared Thermometer",  "HLTH-006",   299m), // 75 → ProdId 76
        ("Automatic Blood Pressure Monitor","HLTH-007", 999m), // 76 → ProdId 77
        ("Muscle Recovery Foam Roller",   "HLTH-008",   349m), // 77 → ProdId 78
        ("Aromatherapy Diffuser",         "HLTH-009",   499m), // 78 → ProdId 79
        ("Melatonin Sleep Supplement",    "HLTH-010",   199m), // 79 → ProdId 80
        ("LEGO City Police Station",      "TOYS-001",  1499m), // 80 → ProdId 81
        ("RC Off-Road Car 1:16",          "TOYS-002",   799m), // 81 → ProdId 82
        ("Classic Monopoly Board Game",   "TOYS-003",   499m), // 82 → ProdId 83
        ("Giant Stuffed Teddy Bear",      "TOYS-004",   399m), // 83 → ProdId 84
        ("Premium 1000-Piece Puzzle",     "TOYS-005",   249m), // 84 → ProdId 85
        ("Watercolor Art Set 48pc",       "TOYS-006",   349m), // 85 → ProdId 86
        ("Junior Science Kit",            "TOYS-007",   449m), // 86 → ProdId 87
        ("Wooden Building Blocks 50pc",   "TOYS-008",   299m), // 87 → ProdId 88
        ("Deluxe Doll House",             "TOYS-009",  1299m), // 88 → ProdId 89
        ("Elite Nerf Blaster Set",        "TOYS-010",   649m), // 89 → ProdId 90
        ("Organic Fair Trade Coffee 500g","FOOD-001",   199m), // 90 → ProdId 91
        ("Premium Dark Chocolate Box",    "FOOD-002",   149m), // 91 → ProdId 92
        ("Raw Honey 1kg",                 "FOOD-003",   249m), // 92 → ProdId 93
        ("Mixed Nuts and Seeds 500g",     "FOOD-004",   189m), // 93 → ProdId 94
        ("Premium Green Tea 50 Bags",     "FOOD-005",   149m), // 94 → ProdId 95
        ("Artisan Pasta Variety Box",     "FOOD-006",   249m), // 95 → ProdId 96
        ("Extra Virgin Olive Oil 1L",     "FOOD-007",   199m), // 96 → ProdId 97
        ("Granola Breakfast Mix 500g",    "FOOD-008",   169m), // 97 → ProdId 98
        ("Protein Snack Bars 12-Pack",    "FOOD-009",   299m), // 98 → ProdId 99
        ("World Spice Collection Set",    "FOOD-010",   449m), // 99 → ProdId 100
    };

    public static void Seed(ModelBuilder modelBuilder)
    {
        SeedAddresses(modelBuilder);
        SeedOrders(modelBuilder);
        SeedOrderItems(modelBuilder);
        SeedPayments(modelBuilder);
    }

    private static void SeedAddresses(ModelBuilder modelBuilder)
    {
        var now = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<Address>().HasData(
            new Address { Id = 1, UserId = 3, Label = "Home", FullName = "Customer003 Demo", Line1 = "123 Main St",  City = "Cape Town",      State = "Western Cape",  PostalCode = "8001", Country = "South Africa", IsDefault = true, CreatedAt = now, UpdatedAt = now },
            new Address { Id = 2, UserId = 4, Label = "Home", FullName = "Customer004 Demo", Line1 = "456 Oak Ave",  City = "Johannesburg",   State = "Gauteng",       PostalCode = "2001", Country = "South Africa", IsDefault = true, CreatedAt = now, UpdatedAt = now },
            new Address { Id = 3, UserId = 5, Label = "Home", FullName = "Customer005 Demo", Line1 = "789 Pine Rd",  City = "Durban",         State = "KwaZulu-Natal", PostalCode = "4001", Country = "South Africa", IsDefault = true, CreatedAt = now, UpdatedAt = now },
            new Address { Id = 4, UserId = 6, Label = "Home", FullName = "Customer006 Demo", Line1 = "321 Elm St",   City = "Pretoria",       State = "Gauteng",       PostalCode = "0001", Country = "South Africa", IsDefault = true, CreatedAt = now, UpdatedAt = now },
            new Address { Id = 5, UserId = 7, Label = "Home", FullName = "Customer007 Demo", Line1 = "654 Maple Dr", City = "Port Elizabeth", State = "Eastern Cape",  PostalCode = "6001", Country = "South Africa", IsDefault = true, CreatedAt = now, UpdatedAt = now }
        );
    }

    private static void SeedOrders(ModelBuilder modelBuilder)
    {
        var orders = new List<Order>();

        for (int i = 1; i <= 200; i++)
        {
            var date = GetOrderDate(i);
            int month = date.Month;
            string status = i <= 120 ? "Delivered" : (i <= 160 ? "Shipped" : (i <= 180 ? "Processing" : (i <= 190 ? "Cancelled" : "Refunded")));
            int userId    = ((i - 1) % 5) + 3;
            int addressId = ((i - 1) % 5) + 1;

            var (subTotal, shippingCost, taxAmount, totalAmount) = ComputeAmounts(i);

            orders.Add(new Order
            {
                Id                = i,
                OrderNumber       = $"ORD-{date:yyyyMM}-{i:D5}",
                UserId            = userId,
                Status            = status,
                ShippingAddressId = addressId,
                SubTotal          = subTotal,
                ShippingCost      = shippingCost,
                TaxAmount         = taxAmount,
                TotalAmount       = totalAmount,
                CreatedAt         = date,
                UpdatedAt         = date,
                ShippedAt         = (status == "Shipped" || status == "Delivered") ? date.AddDays(2) : (DateTime?)null,
                DeliveredAt       = status == "Delivered" ? date.AddDays(5) : (DateTime?)null,
                CancelledAt       = status == "Cancelled" ? date.AddDays(1) : (DateTime?)null,
                CancelReason      = status == "Cancelled" ? "Customer requested cancellation" : null,
            });
        }

        modelBuilder.Entity<Order>().HasData(orders.ToArray());
    }

    private static void SeedOrderItems(ModelBuilder modelBuilder)
    {
        var items = new List<OrderItem>();
        int itemId = 1;

        for (int i = 1; i <= 200; i++)
        {
            var date     = GetOrderDate(i);
            int prodIdx1 = (i - 1) % 100;
            int prodId1  = prodIdx1 + 1;
            var (name1, sku1, price1) = ProductRef[prodIdx1];

            items.Add(new OrderItem
            {
                Id          = itemId++,
                OrderId     = i,
                ProductId   = prodId1,
                ProductName = name1,
                SKU         = sku1,
                Quantity    = 1,
                UnitPrice   = price1,
                TotalPrice  = price1,
                CreatedAt   = date,
            });

            if (i > 100)
            {
                int prodIdx2 = (i + 49) % 100;
                int prodId2  = prodIdx2 + 1;
                var (name2, sku2, price2) = ProductRef[prodIdx2];

                items.Add(new OrderItem
                {
                    Id          = itemId++,
                    OrderId     = i,
                    ProductId   = prodId2,
                    ProductName = name2,
                    SKU         = sku2,
                    Quantity    = 1,
                    UnitPrice   = price2,
                    TotalPrice  = price2,
                    CreatedAt   = date,
                });
            }
        }

        modelBuilder.Entity<OrderItem>().HasData(items.ToArray());
    }

    private static void SeedPayments(ModelBuilder modelBuilder)
    {
        var payments = new List<Payment>();

        for (int i = 1; i <= 160; i++)
        {
            var date = GetOrderDate(i);
            var (_, shippingCost, taxAmount, totalAmount) = ComputeAmounts(i);

            payments.Add(new Payment
            {
                Id       = i,
                OrderId  = i,
                Method   = "MockCard",
                Status   = "Completed",
                Amount   = totalAmount,
                Currency = "ZAR",
                PaidAt   = date.AddDays(1),
                CreatedAt= date,
                UpdatedAt= date,
            });
        }

        modelBuilder.Entity<Payment>().HasData(payments.ToArray());
    }

    private static DateTime GetOrderDate(int orderId)
    {
        const int orderCount = 200;
        var totalDays = (ReportingWindowEnd - ReportingWindowStart).Days;
        var offsetDays = (int)Math.Round((orderId - 1) * (totalDays / (double)(orderCount - 1)));

        return ReportingWindowStart.AddDays(offsetDays);
    }

    private static (decimal SubTotal, decimal ShippingCost, decimal TaxAmount, decimal TotalAmount) ComputeAmounts(int orderId)
    {
        int prodIdx1    = (orderId - 1) % 100;
        decimal price1  = ProductRef[prodIdx1].Price;
        decimal subTotal;

        if (orderId <= 100)
        {
            subTotal = price1;
        }
        else
        {
            int prodIdx2   = (orderId + 49) % 100;
            decimal price2 = ProductRef[prodIdx2].Price;
            subTotal       = price1 + price2;
        }

        decimal shippingCost = 79m;
        decimal taxAmount    = Math.Round(subTotal * 0.15m, 2);
        decimal totalAmount  = subTotal + shippingCost + taxAmount;
        return (subTotal, shippingCost, taxAmount, totalAmount);
    }
}
