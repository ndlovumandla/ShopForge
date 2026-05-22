using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopForge.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BannerSlides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SubTitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LinkUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ButtonText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartsAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerSlides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DiscountType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumOrderAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaximumDiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UsageLimit = table.Column<int>(type: "int", nullable: true),
                    UsageCount = table.Column<int>(type: "int", nullable: false),
                    StartsAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimatedDaysMin = table.Column<int>(type: "int", nullable: false),
                    EstimatedDaysMax = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FreeShippingThreshold = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.CheckConstraint("CK_Users_Role", "[Role] IN ('Admin', 'Manager', 'Customer')");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompareAtPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    LowStockThreshold = table.Column<int>(type: "int", nullable: false),
                    TrackInventory = table.Column<bool>(type: "bit", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Depth = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    IsDigital = table.Column<bool>(type: "bit", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Line1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Line2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ActionUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AttributeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AttributeValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AltText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wishlists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShippingAddressId = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CouponId = table.Column<int>(type: "int", nullable: true),
                    CouponCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TrackingNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ShippedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductVariantId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductVariantId = table.Column<int>(type: "int", nullable: true),
                    ChangeAmount = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ReferenceId = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ChangedByUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryLogs_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_InventoryLogs_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryLogs_Users_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductVariantId = table.Column<int>(type: "int", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VariantName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ChangedByUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderStatusHistories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderStatusHistories_Users_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CardLastFour = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    CardBrand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailureReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<byte>(type: "tinyint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsVerifiedPurchase = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    HelpfulCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "Id", "Description", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { 1, "The name of the store", "StoreName", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ShopForge" },
                    { 2, "Store contact email", "StoreEmail", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hello@shopforge.co.za" },
                    { 3, "Store currency", "Currency", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR" },
                    { 4, "Tax rate (15% VAT)", "TaxRate", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "0.15" },
                    { 5, "Free shipping on orders above this amount", "FreeShippingThreshold", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "999.00" },
                    { 6, "Enable maintenance mode", "MaintenanceMode", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "false" },
                    { 7, "Default low stock alert threshold", "DefaultLowStockThreshold", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "10" }
                });

            migrationBuilder.InsertData(
                table: "BannerSlides",
                columns: new[] { "Id", "ButtonText", "CreatedAt", "DisplayOrder", "ExpiresAt", "ImageUrl", "IsActive", "LinkUrl", "StartsAt", "SubTitle", "Title" },
                values: new object[,]
                {
                    { 1, "Shop Now", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, "https://picsum.photos/seed/banner1/1200/400", true, "/categories/electronics", null, "Up to 50% Off!", "Summer Sale" },
                    { 2, "Explore Now", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, null, "https://picsum.photos/seed/banner2/1200/400", true, "/categories/clothing", null, "Fresh styles just landed", "New Arrivals in Clothing" },
                    { 3, "Start Shopping", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, null, "https://picsum.photos/seed/banner3/1200/400", true, "/shop", null, "On orders over R999", "Free Shipping" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedAt", "IsActive", "LogoUrl", "Name", "UpdatedAt", "Website" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "TechPro", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "UrbanWear", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "HomeStyle", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "ActiveGear", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "ReadMore", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "PureLife", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "PlayZone", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "FreshMart", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "ProBrand", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "ValueChoice", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayOrder", "ImageUrl", "IsActive", "Name", "ParentCategoryId", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Electronic devices and accessories", 1, null, true, "Electronics", null, "electronics", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fashion clothing and apparel", 2, null, true, "Clothing & Apparel", null, "clothing", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Home decor and garden products", 3, null, true, "Home & Garden", null, "home-garden", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sports and outdoor equipment", 4, null, true, "Sports & Outdoors", null, "sports-outdoors", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Books, music, and media", 5, null, true, "Books & Media", null, "books-media", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Health and beauty products", 6, null, true, "Health & Beauty", null, "health-beauty", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Toys and games for all ages", 7, null, true, "Toys & Games", null, "toys-games", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Food and grocery items", 8, null, true, "Food & Grocery", null, "food-grocery", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "DiscountType", "DiscountValue", "ExpiresAt", "IsActive", "MaximumDiscountAmount", "MinimumOrderAmount", "StartsAt", "UpdatedAt", "UsageCount", "UsageLimit" },
                values: new object[,]
                {
                    { 1, "WELCOME10", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "10% off your first order", "Percentage", 10m, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, 1000 },
                    { 2, "SAVE50", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "R50 off orders over R500", "FixedAmount", 50m, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 500m, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, 500 },
                    { 3, "FREESHIP", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Free shipping on orders over R300", "FreeShipping", 0m, null, true, null, 300m, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, null }
                });

            migrationBuilder.InsertData(
                table: "ShippingMethods",
                columns: new[] { "Id", "Cost", "CreatedAt", "Description", "EstimatedDaysMax", "EstimatedDaysMin", "FreeShippingThreshold", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, 79m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Delivered in 3-5 business days", 5, 3, 999m, true, "Standard Delivery" },
                    { 2, 149m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Delivered in 1-2 business days", 2, 1, null, true, "Express Delivery" },
                    { 3, 299m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Delivered next business day", 1, 1, null, true, "Overnight Courier" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "EmailVerified", "FirstName", "IsActive", "LastLoginAt", "LastName", "PasswordHash", "PhoneNumber", "ProfileImageUrl", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@shopforge.co.za", true, "Admin", true, null, "User", "$2a$04$eGWwwzjZU8ow6Uq7JfH5wusVobYEYHCiFG.2OUCjrDME9jdF3F/bG", null, null, "Admin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "manager@shopforge.co.za", true, "Manager", true, null, "User", "$2a$04$QE50Atk1kWG8NVoEWrQ9outpgZ/L7CPMO9W/D5eyT8UPg5vrFOFd6", null, null, "Manager", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "john.doe@example.com", true, "John", true, null, "Doe", "$2a$04$mztCitgMJViuo3FpY4pI3OJPNTtQRKzEo5ont6NgLWQbdc.X7ZTZi", null, null, "Customer", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "jane.smith@example.com", true, "Jane", true, null, "Smith", "$2a$04$mztCitgMJViuo3FpY4pI3OJPNTtQRKzEo5ont6NgLWQbdc.X7ZTZi", null, null, "Customer", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "bob.jones@example.com", true, "Bob", true, null, "Jones", "$2a$04$mztCitgMJViuo3FpY4pI3OJPNTtQRKzEo5ont6NgLWQbdc.X7ZTZi", null, null, "Customer", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "alice.brown@example.com", true, "Alice", true, null, "Brown", "$2a$04$mztCitgMJViuo3FpY4pI3OJPNTtQRKzEo5ont6NgLWQbdc.X7ZTZi", null, null, "Customer", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "charlie.davis@example.com", true, "Charlie", true, null, "Davis", "$2a$04$mztCitgMJViuo3FpY4pI3OJPNTtQRKzEo5ont6NgLWQbdc.X7ZTZi", null, null, "Customer", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "FullName", "IsDefault", "Label", "Line1", "Line2", "PhoneNumber", "PostalCode", "State", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Cape Town", "South Africa", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "John Doe", true, "Home", "123 Main St", null, null, "8001", "Western Cape", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 2, "Johannesburg", "South Africa", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Jane Smith", true, "Home", "456 Oak Ave", null, null, "2001", "Gauteng", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 3, "Durban", "South Africa", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bob Jones", true, "Home", "789 Pine Rd", null, null, "4001", "KwaZulu-Natal", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 4, "Pretoria", "South Africa", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Alice Brown", true, "Home", "321 Elm St", null, null, "0001", "Gauteng", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 5, "Port Elizabeth", "South Africa", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Charlie Davis", true, "Home", "654 Maple Dr", null, null, "6001", "Eastern Cape", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Barcode", "BrandId", "CategoryId", "CompareAtPrice", "CostPrice", "CreatedAt", "Depth", "Description", "Height", "IsActive", "IsDigital", "IsFeatured", "LowStockThreshold", "MetaDescription", "MetaTitle", "Name", "Price", "SKU", "ShortDescription", "Slug", "StockQuantity", "Tags", "TrackInventory", "UpdatedAt", "Weight", "Width" },
                values: new object[,]
                {
                    { 1, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, true, 10, null, null, "4K Smart TV 55\"", 15999m, "ELEC-001", null, "4k-smart-tv-55", 25, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 2, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Laptop Pro 15\"", 22999m, "ELEC-002", null, "laptop-pro-15", 15, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 3, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Wireless Headphones", 2499m, "ELEC-003", null, "wireless-headphones", 50, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 4, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Smartphone X12", 12999m, "ELEC-004", null, "smartphone-x12", 30, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 5, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Bluetooth Speaker", 1299m, "ELEC-005", null, "bluetooth-speaker", 60, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 6, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Gaming Console", 9999m, "ELEC-006", null, "gaming-console", 20, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 7, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Tablet 10\"", 6999m, "ELEC-007", null, "tablet-10", 35, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 8, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Smartwatch Series 5", 3999m, "ELEC-008", null, "smartwatch-series-5", 40, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 9, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Wireless Earbuds", 1999m, "ELEC-009", null, "wireless-earbuds", 70, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 10, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Digital Camera 24MP", 8999m, "ELEC-010", null, "digital-camera-24mp", 18, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 11, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "USB-C Hub 7-in-1", 599m, "ELEC-011", null, "usb-c-hub-7in1", 80, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 12, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Mechanical Keyboard", 1499m, "ELEC-012", null, "mechanical-keyboard", 45, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 13, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Gaming Mouse", 899m, "ELEC-013", null, "gaming-mouse", 55, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 14, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "27\" Monitor", 5999m, "ELEC-014", null, "monitor-27", 22, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 15, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Portable Charger 20000mAh", 499m, "ELEC-015", null, "portable-charger-20000mah", 90, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 16, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Wi-Fi Router AC3000", 1899m, "ELEC-016", null, "wifi-router-ac3000", 28, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 17, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "NAS Drive 4TB", 4999m, "ELEC-017", null, "nas-drive-4tb", 12, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 18, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "VR Headset", 7999m, "ELEC-018", null, "vr-headset", 10, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 19, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Drone with Camera", 11999m, "ELEC-019", null, "drone-with-camera", 8, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 20, null, 1, 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Smart Home Hub", 1299m, "ELEC-020", null, "smart-home-hub", 35, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 21, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, true, 10, null, null, "Men's Slim Fit Jeans", 699m, "CLTH-001", null, "mens-slim-fit-jeans", 80, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 22, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Women's Summer Dress", 599m, "CLTH-002", null, "womens-summer-dress", 60, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 23, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Unisex Hoodie", 499m, "CLTH-003", null, "unisex-hoodie", 100, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 24, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Men's Business Shirt", 449m, "CLTH-004", null, "mens-business-shirt", 70, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 25, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Women's Yoga Leggings", 399m, "CLTH-005", null, "womens-yoga-leggings", 90, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 26, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Men's Polo Shirt", 349m, "CLTH-006", null, "mens-polo-shirt", 85, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 27, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Women's Blazer", 899m, "CLTH-007", null, "womens-blazer", 40, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 28, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Men's Chino Pants", 599m, "CLTH-008", null, "mens-chino-pants", 65, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 29, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Women's Denim Jacket", 799m, "CLTH-009", null, "womens-denim-jacket", 50, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 30, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Men's Cargo Shorts", 449m, "CLTH-010", null, "mens-cargo-shorts", 75, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 31, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Women's Cardigan", 549m, "CLTH-011", null, "womens-cardigan", 55, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 32, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Men's V-Neck Sweater", 499m, "CLTH-012", null, "mens-vneck-sweater", 60, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 33, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Women's Mini Skirt", 349m, "CLTH-013", null, "womens-mini-skirt", 70, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 34, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Men's Tracksuit", 799m, "CLTH-014", null, "mens-tracksuit", 45, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 35, null, 2, 2, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Women's Windbreaker", 699m, "CLTH-015", null, "womens-windbreaker", 40, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 36, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, true, 10, null, null, "Coffee Table Oak", 2999m, "HOME-001", null, "coffee-table-oak", 15, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 37, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Queen Bed Frame", 4999m, "HOME-002", null, "queen-bed-frame", 10, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 38, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Floor Lamp Modern", 799m, "HOME-003", null, "floor-lamp-modern", 30, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 39, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Wall Clock Minimalist", 349m, "HOME-004", null, "wall-clock-minimalist", 50, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 40, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Throw Pillow Set 4pc", 299m, "HOME-005", null, "throw-pillow-set-4pc", 60, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 41, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Kitchen Knife Set 7pc", 999m, "HOME-006", null, "kitchen-knife-set-7pc", 35, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 42, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Non-Stick Cookware Set", 1799m, "HOME-007", null, "non-stick-cookware-set", 20, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 43, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Garden Hose 30m", 399m, "HOME-008", null, "garden-hose-30m", 40, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 44, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Ceramic Indoor Plant Pot", 199m, "HOME-009", null, "ceramic-plant-pot", 80, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 45, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Blackout Curtain Set", 599m, "HOME-010", null, "blackout-curtain-set", 45, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 46, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Bath Towel Set 6pc", 449m, "HOME-011", null, "bath-towel-set-6pc", 55, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 47, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Waterproof Shower Curtain", 249m, "HOME-012", null, "waterproof-shower-curtain", 65, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 48, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Bamboo Desk Organizer", 299m, "HOME-013", null, "bamboo-desk-organizer", 70, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 49, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Bookshelf 5-Tier", 1499m, "HOME-014", null, "bookshelf-5-tier", 18, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 50, null, 3, 3, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Non-Slip Doormat", 149m, "HOME-015", null, "non-slip-doormat", 90, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 51, null, 4, 4, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, true, 10, null, null, "Premium Yoga Mat", 399m, "SPRT-001", null, "premium-yoga-mat", 60, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 52, null, 4, 4, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Adjustable Dumbbell Set 20kg", 1999m, "SPRT-002", null, "adjustable-dumbbell-set", 20, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 53, null, 4, 4, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Trail Running Shoes", 1299m, "SPRT-003", null, "trail-running-shoes", 40, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 54, null, 4, 4, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Sports Gym Bag 40L", 549m, "SPRT-004", null, "sports-gym-bag-40l", 55, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 55, null, 4, 4, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Resistance Bands Set 5pc", 249m, "SPRT-005", null, "resistance-bands-set", 80, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 56, null, 4, 4, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Speed Jump Rope", 149m, "SPRT-006", null, "speed-jump-rope", 100, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 57, null, 4, 4, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Cycling Safety Helmet", 799m, "SPRT-007", null, "cycling-safety-helmet", 30, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 58, null, 4, 4, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Insulated Water Bottle 1L", 299m, "SPRT-008", null, "insulated-water-bottle-1l", 75, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 59, null, 4, 4, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Tennis Racket Pro", 899m, "SPRT-009", null, "tennis-racket-pro", 25, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 60, null, 4, 4, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Match Football Size 5", 349m, "SPRT-010", null, "match-football-size-5", 50, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 61, null, 5, 5, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, true, 10, null, null, "The Art of Clean Coding", 349m, "BOOK-001", null, "art-of-clean-coding", 50, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 62, null, 5, 5, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Business Strategy 101", 299m, "BOOK-002", null, "business-strategy-101", 45, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 63, null, 5, 5, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Mindfulness for Beginners", 249m, "BOOK-003", null, "mindfulness-for-beginners", 60, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 64, null, 5, 5, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "The Cooking Masterclass", 449m, "BOOK-004", null, "cooking-masterclass", 35, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 65, null, 5, 5, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "World History Encyclopedia", 699m, "BOOK-005", null, "world-history-encyclopedia", 20, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 66, null, 5, 5, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Science Fiction Anthology", 199m, "BOOK-006", null, "science-fiction-anthology", 70, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 67, null, 5, 5, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Children's Adventure Series", 249m, "BOOK-007", null, "childrens-adventure-series", 55, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 68, null, 5, 5, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Photography Basics Guide", 349m, "BOOK-008", null, "photography-basics-guide", 30, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 69, null, 5, 5, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Financial Freedom Guide", 299m, "BOOK-009", null, "financial-freedom-guide", 40, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 70, null, 5, 5, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Language Learning Pack", 549m, "BOOK-010", null, "language-learning-pack", 25, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 71, null, 6, 6, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, true, 10, null, null, "Daily Multivitamin Complex", 299m, "HLTH-001", null, "daily-multivitamin-complex", 80, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 72, null, 6, 6, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Whey Protein Powder 1kg", 699m, "HLTH-002", null, "whey-protein-powder-1kg", 50, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 73, null, 6, 6, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Face Moisturizer SPF30", 349m, "HLTH-003", null, "face-moisturizer-spf30", 70, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 74, null, 6, 6, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Hair Growth Serum", 449m, "HLTH-004", null, "hair-growth-serum", 45, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 75, null, 6, 6, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Sonic Electric Toothbrush", 799m, "HLTH-005", null, "sonic-electric-toothbrush", 35, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 76, null, 6, 6, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Digital Infrared Thermometer", 299m, "HLTH-006", null, "digital-infrared-thermometer", 60, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 77, null, 6, 6, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Automatic Blood Pressure Monitor", 999m, "HLTH-007", null, "blood-pressure-monitor", 25, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 78, null, 6, 6, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Muscle Recovery Foam Roller", 349m, "HLTH-008", null, "muscle-recovery-foam-roller", 55, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 79, null, 6, 6, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Aromatherapy Diffuser", 499m, "HLTH-009", null, "aromatherapy-diffuser", 40, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 80, null, 6, 6, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Melatonin Sleep Supplement", 199m, "HLTH-010", null, "melatonin-sleep-supplement", 75, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 81, null, 7, 7, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, true, 10, null, null, "LEGO City Police Station", 1499m, "TOYS-001", null, "lego-city-police-station", 20, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 82, null, 7, 7, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "RC Off-Road Car 1:16", 799m, "TOYS-002", null, "rc-off-road-car", 30, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 83, null, 7, 7, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Classic Monopoly Board Game", 499m, "TOYS-003", null, "classic-monopoly-game", 50, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 84, null, 7, 7, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Giant Stuffed Teddy Bear", 399m, "TOYS-004", null, "giant-stuffed-teddy-bear", 35, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 85, null, 7, 7, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Premium 1000-Piece Puzzle", 249m, "TOYS-005", null, "premium-1000-piece-puzzle", 60, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 86, null, 7, 7, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Watercolor Art Set 48pc", 349m, "TOYS-006", null, "watercolor-art-set-48pc", 45, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 87, null, 7, 7, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Junior Science Kit", 449m, "TOYS-007", null, "junior-science-kit", 25, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 88, null, 7, 7, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Wooden Building Blocks 50pc", 299m, "TOYS-008", null, "wooden-building-blocks", 55, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 89, null, 7, 7, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Deluxe Doll House", 1299m, "TOYS-009", null, "deluxe-doll-house", 15, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 90, null, 7, 7, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Elite Nerf Blaster Set", 649m, "TOYS-010", null, "elite-nerf-blaster-set", 40, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 91, null, 8, 8, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, true, 10, null, null, "Organic Fair Trade Coffee 500g", 199m, "FOOD-001", null, "organic-coffee-500g", 80, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 92, null, 8, 8, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Premium Dark Chocolate Box", 149m, "FOOD-002", null, "premium-dark-chocolate-box", 90, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 93, null, 8, 8, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Raw Honey 1kg", 249m, "FOOD-003", null, "raw-honey-1kg", 70, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 94, null, 8, 8, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Mixed Nuts and Seeds 500g", 189m, "FOOD-004", null, "mixed-nuts-seeds-500g", 85, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 95, null, 8, 8, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Premium Green Tea 50 Bags", 149m, "FOOD-005", null, "premium-green-tea-50bags", 100, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 96, null, 8, 8, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Artisan Pasta Variety Box", 249m, "FOOD-006", null, "artisan-pasta-variety-box", 75, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 97, null, 8, 8, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Extra Virgin Olive Oil 1L", 199m, "FOOD-007", null, "extra-virgin-olive-oil-1l", 80, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 98, null, 8, 8, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Granola Breakfast Mix 500g", 169m, "FOOD-008", null, "granola-breakfast-mix-500g", 90, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 99, null, 8, 8, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "Protein Snack Bars 12-Pack", 299m, "FOOD-009", null, "protein-snack-bars-12pack", 65, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { 100, null, 8, 8, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, false, 10, null, null, "World Spice Collection Set", 449m, "FOOD-010", null, "world-spice-collection-set", 50, null, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CancelReason", "CancelledAt", "CouponCode", "CouponId", "CreatedAt", "DeliveredAt", "DiscountAmount", "Notes", "OrderNumber", "ShippedAt", "ShippingAddressId", "ShippingCost", "Status", "SubTotal", "TaxAmount", "TotalAmount", "TrackingNumber", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, null, null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202401-00001", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 15999m, 2399.85m, 18477.85m, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 2, null, null, null, null, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202402-00002", new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 22999m, 3449.85m, 26527.85m, null, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 3, null, null, null, null, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202403-00003", new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 2499m, 374.85m, 2952.85m, null, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 4, null, null, null, null, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202404-00004", new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 12999m, 1949.85m, 15027.85m, null, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 5, null, null, null, null, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202405-00005", new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 1299m, 194.85m, 1572.85m, null, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 6, null, null, null, null, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202406-00006", new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 9999m, 1499.85m, 11577.85m, null, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 7, null, null, null, null, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202407-00007", new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 6999m, 1049.85m, 8127.85m, null, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 8, null, null, null, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202408-00008", new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 3999m, 599.85m, 4677.85m, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 9, null, null, null, null, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202409-00009", new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 1999m, 299.85m, 2377.85m, null, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 10, null, null, null, null, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202410-00010", new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 8999m, 1349.85m, 10427.85m, null, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 11, null, null, null, null, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202411-00011", new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 599m, 89.85m, 767.85m, null, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 12, null, null, null, null, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202412-00012", new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 1499m, 224.85m, 1802.85m, null, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 13, null, null, null, null, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202401-00013", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 899m, 134.85m, 1112.85m, null, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 14, null, null, null, null, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202402-00014", new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 5999m, 899.85m, 6977.85m, null, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 15, null, null, null, null, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202403-00015", new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 499m, 74.85m, 652.85m, null, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 16, null, null, null, null, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202404-00016", new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 1899m, 284.85m, 2262.85m, null, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 17, null, null, null, null, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202405-00017", new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 4999m, 749.85m, 5827.85m, null, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 18, null, null, null, null, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202406-00018", new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 7999m, 1199.85m, 9277.85m, null, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 19, null, null, null, null, new DateTime(2024, 7, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202407-00019", new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 11999m, 1799.85m, 13877.85m, null, new DateTime(2024, 7, 2, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 20, null, null, null, null, new DateTime(2024, 8, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202408-00020", new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 1299m, 194.85m, 1572.85m, null, new DateTime(2024, 8, 2, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 21, null, null, null, null, new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202409-00021", new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 699m, 104.85m, 882.85m, null, new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 22, null, null, null, null, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 7, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202410-00022", new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 599m, 89.85m, 767.85m, null, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 23, null, null, null, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202411-00023", new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 499m, 74.85m, 652.85m, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 24, null, null, null, null, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202412-00024", new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 449m, 67.35m, 595.35m, null, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 25, null, null, null, null, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202401-00025", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 399m, 59.85m, 537.85m, null, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 26, null, null, null, null, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202402-00026", new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 349m, 52.35m, 480.35m, null, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 27, null, null, null, null, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202403-00027", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 899m, 134.85m, 1112.85m, null, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 28, null, null, null, null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202404-00028", new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 599m, 89.85m, 767.85m, null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 29, null, null, null, null, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202405-00029", new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 799m, 119.85m, 997.85m, null, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 30, null, null, null, null, new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202406-00030", new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 449m, 67.35m, 595.35m, null, new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 31, null, null, null, null, new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202407-00031", new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 549m, 82.35m, 710.35m, null, new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 32, null, null, null, null, new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202408-00032", new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 499m, 74.85m, 652.85m, null, new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 33, null, null, null, null, new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202409-00033", new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 349m, 52.35m, 480.35m, null, new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 34, null, null, null, null, new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202410-00034", new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 799m, 119.85m, 997.85m, null, new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 35, null, null, null, null, new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202411-00035", new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 699m, 104.85m, 882.85m, null, new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 36, null, null, null, null, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202412-00036", new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 2999m, 449.85m, 3527.85m, null, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 37, null, null, null, null, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202401-00037", new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 4999m, 749.85m, 5827.85m, null, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 38, null, null, null, null, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202402-00038", new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 799m, 119.85m, 997.85m, null, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 39, null, null, null, null, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202403-00039", new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 349m, 52.35m, 480.35m, null, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 40, null, null, null, null, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202404-00040", new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 299m, 44.85m, 422.85m, null, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 41, null, null, null, null, new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202405-00041", new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 999m, 149.85m, 1227.85m, null, new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 42, null, null, null, null, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202406-00042", new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 1799m, 269.85m, 2147.85m, null, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 43, null, null, null, null, new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202407-00043", new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 399m, 59.85m, 537.85m, null, new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 44, null, null, null, null, new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202408-00044", new DateTime(2024, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 199m, 29.85m, 307.85m, null, new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 45, null, null, null, null, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202409-00045", new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 599m, 89.85m, 767.85m, null, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 46, null, null, null, null, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202410-00046", new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 449m, 67.35m, 595.35m, null, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 47, null, null, null, null, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202411-00047", new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 249m, 37.35m, 365.35m, null, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 48, null, null, null, null, new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202412-00048", new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 299m, 44.85m, 422.85m, null, new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 49, null, null, null, null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202401-00049", new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 1499m, 224.85m, 1802.85m, null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 50, null, null, null, null, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202402-00050", new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 149m, 22.35m, 250.35m, null, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 51, null, null, null, null, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202403-00051", new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 399m, 59.85m, 537.85m, null, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 52, null, null, null, null, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202404-00052", new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 1999m, 299.85m, 2377.85m, null, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 53, null, null, null, null, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202405-00053", new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 1299m, 194.85m, 1572.85m, null, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 54, null, null, null, null, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202406-00054", new DateTime(2024, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 549m, 82.35m, 710.35m, null, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 55, null, null, null, null, new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202407-00055", new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 249m, 37.35m, 365.35m, null, new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 56, null, null, null, null, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202408-00056", new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 149m, 22.35m, 250.35m, null, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 57, null, null, null, null, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202409-00057", new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 799m, 119.85m, 997.85m, null, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 58, null, null, null, null, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202410-00058", new DateTime(2024, 10, 7, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 299m, 44.85m, 422.85m, null, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 59, null, null, null, null, new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202411-00059", new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 899m, 134.85m, 1112.85m, null, new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 60, null, null, null, null, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202412-00060", new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 349m, 52.35m, 480.35m, null, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 61, null, null, null, null, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202401-00061", new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 349m, 52.35m, 480.35m, null, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 62, null, null, null, null, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202402-00062", new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 299m, 44.85m, 422.85m, null, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 63, null, null, null, null, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202403-00063", new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 249m, 37.35m, 365.35m, null, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 64, null, null, null, null, new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202404-00064", new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 449m, 67.35m, 595.35m, null, new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 65, null, null, null, null, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202405-00065", new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 699m, 104.85m, 882.85m, null, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 66, null, null, null, null, new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202406-00066", new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 199m, 29.85m, 307.85m, null, new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 67, null, null, null, null, new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202407-00067", new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 249m, 37.35m, 365.35m, null, new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 68, null, null, null, null, new DateTime(2024, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202408-00068", new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 349m, 52.35m, 480.35m, null, new DateTime(2024, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 69, null, null, null, null, new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202409-00069", new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 299m, 44.85m, 422.85m, null, new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 70, null, null, null, null, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202410-00070", new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 549m, 82.35m, 710.35m, null, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 71, null, null, null, null, new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202411-00071", new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 299m, 44.85m, 422.85m, null, new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 72, null, null, null, null, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202412-00072", new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 699m, 104.85m, 882.85m, null, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 73, null, null, null, null, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202401-00073", new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 349m, 52.35m, 480.35m, null, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 74, null, null, null, null, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202402-00074", new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 449m, 67.35m, 595.35m, null, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 75, null, null, null, null, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202403-00075", new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 799m, 119.85m, 997.85m, null, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 76, null, null, null, null, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202404-00076", new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 299m, 44.85m, 422.85m, null, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 77, null, null, null, null, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202405-00077", new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 999m, 149.85m, 1227.85m, null, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 78, null, null, null, null, new DateTime(2024, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202406-00078", new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 349m, 52.35m, 480.35m, null, new DateTime(2024, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 79, null, null, null, null, new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202407-00079", new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 499m, 74.85m, 652.85m, null, new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 80, null, null, null, null, new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202408-00080", new DateTime(2024, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 199m, 29.85m, 307.85m, null, new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 81, null, null, null, null, new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202409-00081", new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 1499m, 224.85m, 1802.85m, null, new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 82, null, null, null, null, new DateTime(2024, 10, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202410-00082", new DateTime(2024, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 799m, 119.85m, 997.85m, null, new DateTime(2024, 10, 7, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 83, null, null, null, null, new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202411-00083", new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 499m, 74.85m, 652.85m, null, new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 84, null, null, null, null, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202412-00084", new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 399m, 59.85m, 537.85m, null, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 85, null, null, null, null, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202401-00085", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 249m, 37.35m, 365.35m, null, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 86, null, null, null, null, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202402-00086", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 349m, 52.35m, 480.35m, null, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 87, null, null, null, null, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202403-00087", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 449m, 67.35m, 595.35m, null, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 88, null, null, null, null, new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202404-00088", new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 299m, 44.85m, 422.85m, null, new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 89, null, null, null, null, new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202405-00089", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 1299m, 194.85m, 1572.85m, null, new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 90, null, null, null, null, new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202406-00090", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 649m, 97.35m, 825.35m, null, new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 91, null, null, null, null, new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 13, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202407-00091", new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 199m, 29.85m, 307.85m, null, new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 92, null, null, null, null, new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202408-00092", new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 149m, 22.35m, 250.35m, null, new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 93, null, null, null, null, new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202409-00093", new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 249m, 37.35m, 365.35m, null, new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 94, null, null, null, null, new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202410-00094", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 189m, 28.35m, 296.35m, null, new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 95, null, null, null, null, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202411-00095", new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 149m, 22.35m, 250.35m, null, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 96, null, null, null, null, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202412-00096", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 249m, 37.35m, 365.35m, null, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 97, null, null, null, null, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202401-00097", new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 199m, 29.85m, 307.85m, null, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 98, null, null, null, null, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202402-00098", new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 169m, 25.35m, 273.35m, null, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 99, null, null, null, null, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202403-00099", new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 299m, 44.85m, 422.85m, null, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 100, null, null, null, null, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202404-00100", new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 449m, 67.35m, 595.35m, null, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 101, null, null, null, null, new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202405-00101", new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 16398m, 2459.70m, 18936.70m, null, new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 102, null, null, null, null, new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202406-00102", new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 24998m, 3749.70m, 28826.70m, null, new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 103, null, null, null, null, new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202407-00103", new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 3798m, 569.70m, 4446.70m, null, new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 104, null, null, null, null, new DateTime(2024, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202408-00104", new DateTime(2024, 8, 11, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 13548m, 2032.20m, 15659.20m, null, new DateTime(2024, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 105, null, null, null, null, new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202409-00105", new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 1548m, 232.20m, 1859.20m, null, new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 106, null, null, null, null, new DateTime(2024, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202410-00106", new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 10148m, 1522.20m, 11749.20m, null, new DateTime(2024, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 107, null, null, null, null, new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 14, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202411-00107", new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 7798m, 1169.70m, 9046.70m, null, new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 108, null, null, null, null, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202412-00108", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 4298m, 644.70m, 5021.70m, null, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 109, null, null, null, null, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202401-00109", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 2898m, 434.70m, 3411.70m, null, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 110, null, null, null, null, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202402-00110", new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 9348m, 1402.20m, 10829.20m, null, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 111, null, null, null, null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202403-00111", new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 948m, 142.20m, 1169.20m, null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 112, null, null, null, null, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202404-00112", new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 1798m, 269.70m, 2146.70m, null, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 113, null, null, null, null, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202405-00113", new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 1148m, 172.20m, 1399.20m, null, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 114, null, null, null, null, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202406-00114", new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 6448m, 967.20m, 7494.20m, null, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 115, null, null, null, null, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202407-00115", new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 1198m, 179.70m, 1456.70m, null, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 116, null, null, null, null, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202408-00116", new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Delivered", 2098m, 314.70m, 2491.70m, null, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 117, null, null, null, null, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202409-00117", new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Delivered", 5248m, 787.20m, 6114.20m, null, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 118, null, null, null, null, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202410-00118", new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Delivered", 8348m, 1252.20m, 9679.20m, null, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 119, null, null, null, null, new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202411-00119", new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Delivered", 12298m, 1844.70m, 14221.70m, null, new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 120, null, null, null, null, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, "ORD-202412-00120", new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Delivered", 1848m, 277.20m, 2204.20m, null, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 121, null, null, null, null, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202401-00121", new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Shipped", 998m, 149.70m, 1226.70m, null, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 122, null, null, null, null, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202402-00122", new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Shipped", 1298m, 194.70m, 1571.70m, null, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 123, null, null, null, null, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202403-00123", new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Shipped", 848m, 127.20m, 1054.20m, null, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 124, null, null, null, null, new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202404-00124", new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Shipped", 898m, 134.70m, 1111.70m, null, new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 125, null, null, null, null, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202405-00125", new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Shipped", 1198m, 179.70m, 1456.70m, null, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 126, null, null, null, null, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202406-00126", new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Shipped", 648m, 97.20m, 824.20m, null, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 127, null, null, null, null, new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202407-00127", new DateTime(2024, 7, 13, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Shipped", 1898m, 284.70m, 2261.70m, null, new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 128, null, null, null, null, new DateTime(2024, 8, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202408-00128", new DateTime(2024, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Shipped", 948m, 142.20m, 1169.20m, null, new DateTime(2024, 8, 11, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 129, null, null, null, null, new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202409-00129", new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Shipped", 1298m, 194.70m, 1571.70m, null, new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 130, null, null, null, null, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202410-00130", new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Shipped", 648m, 97.20m, 824.20m, null, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 131, null, null, null, null, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202411-00131", new DateTime(2024, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Shipped", 2048m, 307.20m, 2434.20m, null, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 132, null, null, null, null, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202412-00132", new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Shipped", 1298m, 194.70m, 1571.70m, null, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 133, null, null, null, null, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202401-00133", new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Shipped", 848m, 127.20m, 1054.20m, null, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 134, null, null, null, null, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202402-00134", new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Shipped", 1198m, 179.70m, 1456.70m, null, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 135, null, null, null, null, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202403-00135", new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Shipped", 948m, 142.20m, 1169.20m, null, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 136, null, null, null, null, new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202404-00136", new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Shipped", 3348m, 502.20m, 3929.20m, null, new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 137, null, null, null, null, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202405-00137", new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Shipped", 5448m, 817.20m, 6344.20m, null, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 138, null, null, null, null, new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202406-00138", new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Shipped", 1098m, 164.70m, 1341.70m, null, new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 139, null, null, null, null, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202407-00139", new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Shipped", 1648m, 247.20m, 1974.20m, null, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 140, null, null, null, null, new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202408-00140", new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Shipped", 948m, 142.20m, 1169.20m, null, new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 141, null, null, null, null, new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202409-00141", new DateTime(2024, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Shipped", 1198m, 179.70m, 1456.70m, null, new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 142, null, null, null, null, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202410-00142", new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Shipped", 1948m, 292.20m, 2319.20m, null, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 143, null, null, null, null, new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202411-00143", new DateTime(2024, 11, 14, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Shipped", 648m, 97.20m, 824.20m, null, new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 144, null, null, null, null, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202412-00144", new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Shipped", 388m, 58.20m, 525.20m, null, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 145, null, null, null, null, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202401-00145", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Shipped", 748m, 112.20m, 939.20m, null, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 146, null, null, null, null, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202402-00146", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Shipped", 698m, 104.70m, 881.70m, null, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 147, null, null, null, null, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202403-00147", new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Shipped", 448m, 67.20m, 594.20m, null, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 148, null, null, null, null, new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202404-00148", new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Shipped", 468m, 70.20m, 617.20m, null, new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 149, null, null, null, null, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202405-00149", new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Shipped", 1798m, 269.70m, 2146.70m, null, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 150, null, null, null, null, new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202406-00150", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Shipped", 598m, 89.70m, 766.70m, null, new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 151, null, null, null, null, new DateTime(2024, 7, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202407-00151", new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Shipped", 16398m, 2459.70m, 18936.70m, null, new DateTime(2024, 7, 13, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 152, null, null, null, null, new DateTime(2024, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202408-00152", new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Shipped", 24998m, 3749.70m, 28826.70m, null, new DateTime(2024, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 153, null, null, null, null, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202409-00153", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Shipped", 3798m, 569.70m, 4446.70m, null, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 154, null, null, null, null, new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202410-00154", new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Shipped", 13548m, 2032.20m, 15659.20m, null, new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 155, null, null, null, null, new DateTime(2024, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202411-00155", new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Shipped", 1548m, 232.20m, 1859.20m, null, new DateTime(2024, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 156, null, null, null, null, new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202412-00156", new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, 79m, "Shipped", 10148m, 1522.20m, 11749.20m, null, new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 157, null, null, null, null, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202401-00157", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), 2, 79m, "Shipped", 7798m, 1169.70m, 9046.70m, null, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 158, null, null, null, null, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202402-00158", new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), 3, 79m, "Shipped", 4298m, 644.70m, 5021.70m, null, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 159, null, null, null, null, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202403-00159", new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Utc), 4, 79m, "Shipped", 2898m, 434.70m, 3411.70m, null, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 160, null, null, null, null, new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202404-00160", new DateTime(2024, 4, 16, 0, 0, 0, 0, DateTimeKind.Utc), 5, 79m, "Shipped", 9348m, 1402.20m, 10829.20m, null, new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 161, null, null, null, null, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202405-00161", null, 1, 79m, "Processing", 948m, 142.20m, 1169.20m, null, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 162, null, null, null, null, new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202406-00162", null, 2, 79m, "Processing", 1798m, 269.70m, 2146.70m, null, new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 163, null, null, null, null, new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202407-00163", null, 3, 79m, "Processing", 1148m, 172.20m, 1399.20m, null, new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 164, null, null, null, null, new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202408-00164", null, 4, 79m, "Processing", 6448m, 967.20m, 7494.20m, null, new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 165, null, null, null, null, new DateTime(2024, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202409-00165", null, 5, 79m, "Processing", 1198m, 179.70m, 1456.70m, null, new DateTime(2024, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 166, null, null, null, null, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202410-00166", null, 1, 79m, "Processing", 2098m, 314.70m, 2491.70m, null, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 167, null, null, null, null, new DateTime(2024, 11, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202411-00167", null, 2, 79m, "Processing", 5248m, 787.20m, 6114.20m, null, new DateTime(2024, 11, 14, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 168, null, null, null, null, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202412-00168", null, 3, 79m, "Processing", 8348m, 1252.20m, 9679.20m, null, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 169, null, null, null, null, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202401-00169", null, 4, 79m, "Processing", 12298m, 1844.70m, 14221.70m, null, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 170, null, null, null, null, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202402-00170", null, 5, 79m, "Processing", 1848m, 277.20m, 2204.20m, null, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 171, null, null, null, null, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202403-00171", null, 1, 79m, "Processing", 998m, 149.70m, 1226.70m, null, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 172, null, null, null, null, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202404-00172", null, 2, 79m, "Processing", 1298m, 194.70m, 1571.70m, null, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 173, null, null, null, null, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202405-00173", null, 3, 79m, "Processing", 848m, 127.20m, 1054.20m, null, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 174, null, null, null, null, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202406-00174", null, 4, 79m, "Processing", 898m, 134.70m, 1111.70m, null, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 175, null, null, null, null, new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202407-00175", null, 5, 79m, "Processing", 1198m, 179.70m, 1456.70m, null, new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 176, null, null, null, null, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202408-00176", null, 1, 79m, "Processing", 648m, 97.20m, 824.20m, null, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 177, null, null, null, null, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202409-00177", null, 2, 79m, "Processing", 1898m, 284.70m, 2261.70m, null, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 178, null, null, null, null, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202410-00178", null, 3, 79m, "Processing", 948m, 142.20m, 1169.20m, null, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 179, null, null, null, null, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202411-00179", null, 4, 79m, "Processing", 1298m, 194.70m, 1571.70m, null, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 180, null, null, null, null, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202412-00180", null, 5, 79m, "Processing", 648m, 97.20m, 824.20m, null, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 181, "Customer requested cancellation", new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, null, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202401-00181", null, 1, 79m, "Cancelled", 2048m, 307.20m, 2434.20m, null, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 182, "Customer requested cancellation", new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, null, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202402-00182", null, 2, 79m, "Cancelled", 1298m, 194.70m, 1571.70m, null, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 183, "Customer requested cancellation", new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, null, new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202403-00183", null, 3, 79m, "Cancelled", 848m, 127.20m, 1054.20m, null, new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 184, "Customer requested cancellation", new DateTime(2024, 4, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, null, new DateTime(2024, 4, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202404-00184", null, 4, 79m, "Cancelled", 1198m, 179.70m, 1456.70m, null, new DateTime(2024, 4, 16, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 185, "Customer requested cancellation", new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, null, new DateTime(2024, 5, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202405-00185", null, 5, 79m, "Cancelled", 948m, 142.20m, 1169.20m, null, new DateTime(2024, 5, 16, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 186, "Customer requested cancellation", new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, null, new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202406-00186", null, 1, 79m, "Cancelled", 3348m, 502.20m, 3929.20m, null, new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 187, "Customer requested cancellation", new DateTime(2024, 7, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, null, new DateTime(2024, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202407-00187", null, 2, 79m, "Cancelled", 5448m, 817.20m, 6344.20m, null, new DateTime(2024, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 188, "Customer requested cancellation", new DateTime(2024, 8, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, null, new DateTime(2024, 8, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202408-00188", null, 3, 79m, "Cancelled", 1098m, 164.70m, 1341.70m, null, new DateTime(2024, 8, 16, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 189, "Customer requested cancellation", new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, null, new DateTime(2024, 9, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202409-00189", null, 4, 79m, "Cancelled", 1648m, 247.20m, 1974.20m, null, new DateTime(2024, 9, 16, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 190, "Customer requested cancellation", new DateTime(2024, 10, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, null, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202410-00190", null, 5, 79m, "Cancelled", 948m, 142.20m, 1169.20m, null, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 191, null, null, null, null, new DateTime(2024, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202411-00191", null, 1, 79m, "Refunded", 1198m, 179.70m, 1456.70m, null, new DateTime(2024, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 192, null, null, null, null, new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202412-00192", null, 2, 79m, "Refunded", 1948m, 292.20m, 2319.20m, null, new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 193, null, null, null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202401-00193", null, 3, 79m, "Refunded", 648m, 97.20m, 824.20m, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 194, null, null, null, null, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202402-00194", null, 4, 79m, "Refunded", 388m, 58.20m, 525.20m, null, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 195, null, null, null, null, new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202403-00195", null, 5, 79m, "Refunded", 748m, 112.20m, 939.20m, null, new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 196, null, null, null, null, new DateTime(2024, 4, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202404-00196", null, 1, 79m, "Refunded", 698m, 104.70m, 881.70m, null, new DateTime(2024, 4, 17, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 197, null, null, null, null, new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202405-00197", null, 2, 79m, "Refunded", 448m, 67.20m, 594.20m, null, new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 198, null, null, null, null, new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202406-00198", null, 3, 79m, "Refunded", 468m, 70.20m, 617.20m, null, new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 199, null, null, null, null, new DateTime(2024, 7, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202407-00199", null, 4, 79m, "Refunded", 1798m, 269.70m, 2146.70m, null, new DateTime(2024, 7, 17, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 200, null, null, null, null, new DateTime(2024, 8, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, null, "ORD-202408-00200", null, 5, 79m, "Refunded", 598m, 89.70m, 766.70m, null, new DateTime(2024, 8, 17, 0, 0, 0, 0, DateTimeKind.Utc), 7 }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "AltText", "CreatedAt", "DisplayOrder", "ImageUrl", "IsPrimary", "ProductId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-001/400/400", true, 1 },
                    { 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-002/400/400", true, 2 },
                    { 3, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-003/400/400", true, 3 },
                    { 4, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-004/400/400", true, 4 },
                    { 5, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-005/400/400", true, 5 },
                    { 6, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-006/400/400", true, 6 },
                    { 7, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-007/400/400", true, 7 },
                    { 8, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-008/400/400", true, 8 },
                    { 9, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-009/400/400", true, 9 },
                    { 10, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-010/400/400", true, 10 },
                    { 11, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-011/400/400", true, 11 },
                    { 12, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-012/400/400", true, 12 },
                    { 13, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-013/400/400", true, 13 },
                    { 14, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-014/400/400", true, 14 },
                    { 15, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-015/400/400", true, 15 },
                    { 16, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-016/400/400", true, 16 },
                    { 17, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-017/400/400", true, 17 },
                    { 18, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-018/400/400", true, 18 },
                    { 19, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-019/400/400", true, 19 },
                    { 20, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/ELEC-020/400/400", true, 20 },
                    { 21, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-001/400/400", true, 21 },
                    { 22, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-002/400/400", true, 22 },
                    { 23, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-003/400/400", true, 23 },
                    { 24, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-004/400/400", true, 24 },
                    { 25, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-005/400/400", true, 25 },
                    { 26, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-006/400/400", true, 26 },
                    { 27, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-007/400/400", true, 27 },
                    { 28, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-008/400/400", true, 28 },
                    { 29, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-009/400/400", true, 29 },
                    { 30, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-010/400/400", true, 30 },
                    { 31, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-011/400/400", true, 31 },
                    { 32, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-012/400/400", true, 32 },
                    { 33, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-013/400/400", true, 33 },
                    { 34, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-014/400/400", true, 34 },
                    { 35, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/CLTH-015/400/400", true, 35 },
                    { 36, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-001/400/400", true, 36 },
                    { 37, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-002/400/400", true, 37 },
                    { 38, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-003/400/400", true, 38 },
                    { 39, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-004/400/400", true, 39 },
                    { 40, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-005/400/400", true, 40 },
                    { 41, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-006/400/400", true, 41 },
                    { 42, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-007/400/400", true, 42 },
                    { 43, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-008/400/400", true, 43 },
                    { 44, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-009/400/400", true, 44 },
                    { 45, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-010/400/400", true, 45 },
                    { 46, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-011/400/400", true, 46 },
                    { 47, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-012/400/400", true, 47 },
                    { 48, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-013/400/400", true, 48 },
                    { 49, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-014/400/400", true, 49 },
                    { 50, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HOME-015/400/400", true, 50 },
                    { 51, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/SPRT-001/400/400", true, 51 },
                    { 52, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/SPRT-002/400/400", true, 52 },
                    { 53, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/SPRT-003/400/400", true, 53 },
                    { 54, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/SPRT-004/400/400", true, 54 },
                    { 55, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/SPRT-005/400/400", true, 55 },
                    { 56, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/SPRT-006/400/400", true, 56 },
                    { 57, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/SPRT-007/400/400", true, 57 },
                    { 58, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/SPRT-008/400/400", true, 58 },
                    { 59, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/SPRT-009/400/400", true, 59 },
                    { 60, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/SPRT-010/400/400", true, 60 },
                    { 61, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/BOOK-001/400/400", true, 61 },
                    { 62, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/BOOK-002/400/400", true, 62 },
                    { 63, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/BOOK-003/400/400", true, 63 },
                    { 64, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/BOOK-004/400/400", true, 64 },
                    { 65, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/BOOK-005/400/400", true, 65 },
                    { 66, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/BOOK-006/400/400", true, 66 },
                    { 67, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/BOOK-007/400/400", true, 67 },
                    { 68, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/BOOK-008/400/400", true, 68 },
                    { 69, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/BOOK-009/400/400", true, 69 },
                    { 70, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/BOOK-010/400/400", true, 70 },
                    { 71, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HLTH-001/400/400", true, 71 },
                    { 72, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HLTH-002/400/400", true, 72 },
                    { 73, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HLTH-003/400/400", true, 73 },
                    { 74, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HLTH-004/400/400", true, 74 },
                    { 75, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HLTH-005/400/400", true, 75 },
                    { 76, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HLTH-006/400/400", true, 76 },
                    { 77, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HLTH-007/400/400", true, 77 },
                    { 78, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HLTH-008/400/400", true, 78 },
                    { 79, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HLTH-009/400/400", true, 79 },
                    { 80, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/HLTH-010/400/400", true, 80 },
                    { 81, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/TOYS-001/400/400", true, 81 },
                    { 82, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/TOYS-002/400/400", true, 82 },
                    { 83, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/TOYS-003/400/400", true, 83 },
                    { 84, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/TOYS-004/400/400", true, 84 },
                    { 85, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/TOYS-005/400/400", true, 85 },
                    { 86, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/TOYS-006/400/400", true, 86 },
                    { 87, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/TOYS-007/400/400", true, 87 },
                    { 88, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/TOYS-008/400/400", true, 88 },
                    { 89, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/TOYS-009/400/400", true, 89 },
                    { 90, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/TOYS-010/400/400", true, 90 },
                    { 91, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/FOOD-001/400/400", true, 91 },
                    { 92, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/FOOD-002/400/400", true, 92 },
                    { 93, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/FOOD-003/400/400", true, 93 },
                    { 94, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/FOOD-004/400/400", true, 94 },
                    { 95, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/FOOD-005/400/400", true, 95 },
                    { 96, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/FOOD-006/400/400", true, 96 },
                    { 97, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/FOOD-007/400/400", true, 97 },
                    { 98, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/FOOD-008/400/400", true, 98 },
                    { 99, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/FOOD-009/400/400", true, 99 },
                    { 100, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "https://picsum.photos/seed/FOOD-010/400/400", true, 100 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CreatedAt", "OrderId", "ProductId", "ProductName", "ProductVariantId", "Quantity", "SKU", "TotalPrice", "UnitPrice", "VariantName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, "4K Smart TV 55\"", null, 1, "ELEC-001", 15999m, 15999m, null },
                    { 2, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, 2, "Laptop Pro 15\"", null, 1, "ELEC-002", 22999m, 22999m, null },
                    { 3, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 3, "Wireless Headphones", null, 1, "ELEC-003", 2499m, 2499m, null },
                    { 4, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 4, "Smartphone X12", null, 1, "ELEC-004", 12999m, 12999m, null },
                    { 5, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, 5, "Bluetooth Speaker", null, 1, "ELEC-005", 1299m, 1299m, null },
                    { 6, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, 6, "Gaming Console", null, 1, "ELEC-006", 9999m, 9999m, null },
                    { 7, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, 7, "Tablet 10\"", null, 1, "ELEC-007", 6999m, 6999m, null },
                    { 8, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, 8, "Smartwatch Series 5", null, 1, "ELEC-008", 3999m, 3999m, null },
                    { 9, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, 9, "Wireless Earbuds", null, 1, "ELEC-009", 1999m, 1999m, null },
                    { 10, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, 10, "Digital Camera 24MP", null, 1, "ELEC-010", 8999m, 8999m, null },
                    { 11, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11, 11, "USB-C Hub 7-in-1", null, 1, "ELEC-011", 599m, 599m, null },
                    { 12, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, 12, "Mechanical Keyboard", null, 1, "ELEC-012", 1499m, 1499m, null },
                    { 13, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), 13, 13, "Gaming Mouse", null, 1, "ELEC-013", 899m, 899m, null },
                    { 14, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), 14, 14, "27\" Monitor", null, 1, "ELEC-014", 5999m, 5999m, null },
                    { 15, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 15, 15, "Portable Charger 20000mAh", null, 1, "ELEC-015", 499m, 499m, null },
                    { 16, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Utc), 16, 16, "Wi-Fi Router AC3000", null, 1, "ELEC-016", 1899m, 1899m, null },
                    { 17, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), 17, 17, "NAS Drive 4TB", null, 1, "ELEC-017", 4999m, 4999m, null },
                    { 18, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), 18, 18, "VR Headset", null, 1, "ELEC-018", 7999m, 7999m, null },
                    { 19, new DateTime(2024, 7, 2, 0, 0, 0, 0, DateTimeKind.Utc), 19, 19, "Drone with Camera", null, 1, "ELEC-019", 11999m, 11999m, null },
                    { 20, new DateTime(2024, 8, 2, 0, 0, 0, 0, DateTimeKind.Utc), 20, 20, "Smart Home Hub", null, 1, "ELEC-020", 1299m, 1299m, null },
                    { 21, new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc), 21, 21, "Men's Slim Fit Jeans", null, 1, "CLTH-001", 699m, 699m, null },
                    { 22, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc), 22, 22, "Women's Summer Dress", null, 1, "CLTH-002", 599m, 599m, null },
                    { 23, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), 23, 23, "Unisex Hoodie", null, 1, "CLTH-003", 499m, 499m, null },
                    { 24, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc), 24, 24, "Men's Business Shirt", null, 1, "CLTH-004", 449m, 449m, null },
                    { 25, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), 25, 25, "Women's Yoga Leggings", null, 1, "CLTH-005", 399m, 399m, null },
                    { 26, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), 26, 26, "Men's Polo Shirt", null, 1, "CLTH-006", 349m, 349m, null },
                    { 27, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), 27, 27, "Women's Blazer", null, 1, "CLTH-007", 899m, 899m, null },
                    { 28, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), 28, 28, "Men's Chino Pants", null, 1, "CLTH-008", 599m, 599m, null },
                    { 29, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), 29, 29, "Women's Denim Jacket", null, 1, "CLTH-009", 799m, 799m, null },
                    { 30, new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), 30, 30, "Men's Cargo Shorts", null, 1, "CLTH-010", 449m, 449m, null },
                    { 31, new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), 31, 31, "Women's Cardigan", null, 1, "CLTH-011", 549m, 549m, null },
                    { 32, new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Utc), 32, 32, "Men's V-Neck Sweater", null, 1, "CLTH-012", 499m, 499m, null },
                    { 33, new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc), 33, 33, "Women's Mini Skirt", null, 1, "CLTH-013", 349m, 349m, null },
                    { 34, new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), 34, 34, "Men's Tracksuit", null, 1, "CLTH-014", 799m, 799m, null },
                    { 35, new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), 35, 35, "Women's Windbreaker", null, 1, "CLTH-015", 699m, 699m, null },
                    { 36, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc), 36, 36, "Coffee Table Oak", null, 1, "HOME-001", 2999m, 2999m, null },
                    { 37, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), 37, 37, "Queen Bed Frame", null, 1, "HOME-002", 4999m, 4999m, null },
                    { 38, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), 38, 38, "Floor Lamp Modern", null, 1, "HOME-003", 799m, 799m, null },
                    { 39, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), 39, 39, "Wall Clock Minimalist", null, 1, "HOME-004", 349m, 349m, null },
                    { 40, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), 40, 40, "Throw Pillow Set 4pc", null, 1, "HOME-005", 299m, 299m, null },
                    { 41, new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), 41, 41, "Kitchen Knife Set 7pc", null, 1, "HOME-006", 999m, 999m, null },
                    { 42, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), 42, 42, "Non-Stick Cookware Set", null, 1, "HOME-007", 1799m, 1799m, null },
                    { 43, new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), 43, 43, "Garden Hose 30m", null, 1, "HOME-008", 399m, 399m, null },
                    { 44, new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), 44, 44, "Ceramic Indoor Plant Pot", null, 1, "HOME-009", 199m, 199m, null },
                    { 45, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc), 45, 45, "Blackout Curtain Set", null, 1, "HOME-010", 599m, 599m, null },
                    { 46, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), 46, 46, "Bath Towel Set 6pc", null, 1, "HOME-011", 449m, 449m, null },
                    { 47, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Utc), 47, 47, "Waterproof Shower Curtain", null, 1, "HOME-012", 249m, 249m, null },
                    { 48, new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Utc), 48, 48, "Bamboo Desk Organizer", null, 1, "HOME-013", 299m, 299m, null },
                    { 49, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), 49, 49, "Bookshelf 5-Tier", null, 1, "HOME-014", 1499m, 1499m, null },
                    { 50, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), 50, 50, "Non-Slip Doormat", null, 1, "HOME-015", 149m, 149m, null },
                    { 51, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), 51, 51, "Premium Yoga Mat", null, 1, "SPRT-001", 399m, 399m, null },
                    { 52, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), 52, 52, "Adjustable Dumbbell Set 20kg", null, 1, "SPRT-002", 1999m, 1999m, null },
                    { 53, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), 53, 53, "Trail Running Shoes", null, 1, "SPRT-003", 1299m, 1299m, null },
                    { 54, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc), 54, 54, "Sports Gym Bag 40L", null, 1, "SPRT-004", 549m, 549m, null },
                    { 55, new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc), 55, 55, "Resistance Bands Set 5pc", null, 1, "SPRT-005", 249m, 249m, null },
                    { 56, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), 56, 56, "Speed Jump Rope", null, 1, "SPRT-006", 149m, 149m, null },
                    { 57, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), 57, 57, "Cycling Safety Helmet", null, 1, "SPRT-007", 799m, 799m, null },
                    { 58, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Utc), 58, 58, "Insulated Water Bottle 1L", null, 1, "SPRT-008", 299m, 299m, null },
                    { 59, new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), 59, 59, "Tennis Racket Pro", null, 1, "SPRT-009", 899m, 899m, null },
                    { 60, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), 60, 60, "Match Football Size 5", null, 1, "SPRT-010", 349m, 349m, null },
                    { 61, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), 61, 61, "The Art of Clean Coding", null, 1, "BOOK-001", 349m, 349m, null },
                    { 62, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), 62, 62, "Business Strategy 101", null, 1, "BOOK-002", 299m, 299m, null },
                    { 63, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), 63, 63, "Mindfulness for Beginners", null, 1, "BOOK-003", 249m, 249m, null },
                    { 64, new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Utc), 64, 64, "The Cooking Masterclass", null, 1, "BOOK-004", 449m, 449m, null },
                    { 65, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), 65, 65, "World History Encyclopedia", null, 1, "BOOK-005", 699m, 699m, null },
                    { 66, new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc), 66, 66, "Science Fiction Anthology", null, 1, "BOOK-006", 199m, 199m, null },
                    { 67, new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), 67, 67, "Children's Adventure Series", null, 1, "BOOK-007", 249m, 249m, null },
                    { 68, new DateTime(2024, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc), 68, 68, "Photography Basics Guide", null, 1, "BOOK-008", 349m, 349m, null },
                    { 69, new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Utc), 69, 69, "Financial Freedom Guide", null, 1, "BOOK-009", 299m, 299m, null },
                    { 70, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), 70, 70, "Language Learning Pack", null, 1, "BOOK-010", 549m, 549m, null },
                    { 71, new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc), 71, 71, "Daily Multivitamin Complex", null, 1, "HLTH-001", 299m, 299m, null },
                    { 72, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc), 72, 72, "Whey Protein Powder 1kg", null, 1, "HLTH-002", 699m, 699m, null },
                    { 73, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), 73, 73, "Face Moisturizer SPF30", null, 1, "HLTH-003", 349m, 349m, null },
                    { 74, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), 74, 74, "Hair Growth Serum", null, 1, "HLTH-004", 449m, 449m, null },
                    { 75, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), 75, 75, "Sonic Electric Toothbrush", null, 1, "HLTH-005", 799m, 799m, null },
                    { 76, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Utc), 76, 76, "Digital Infrared Thermometer", null, 1, "HLTH-006", 299m, 299m, null },
                    { 77, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), 77, 77, "Automatic Blood Pressure Monitor", null, 1, "HLTH-007", 999m, 999m, null },
                    { 78, new DateTime(2024, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), 78, 78, "Muscle Recovery Foam Roller", null, 1, "HLTH-008", 349m, 349m, null },
                    { 79, new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), 79, 79, "Aromatherapy Diffuser", null, 1, "HLTH-009", 499m, 499m, null },
                    { 80, new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), 80, 80, "Melatonin Sleep Supplement", null, 1, "HLTH-010", 199m, 199m, null },
                    { 81, new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Utc), 81, 81, "LEGO City Police Station", null, 1, "TOYS-001", 1499m, 1499m, null },
                    { 82, new DateTime(2024, 10, 7, 0, 0, 0, 0, DateTimeKind.Utc), 82, 82, "RC Off-Road Car 1:16", null, 1, "TOYS-002", 799m, 799m, null },
                    { 83, new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), 83, 83, "Classic Monopoly Board Game", null, 1, "TOYS-003", 499m, 499m, null },
                    { 84, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), 84, 84, "Giant Stuffed Teddy Bear", null, 1, "TOYS-004", 399m, 399m, null },
                    { 85, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), 85, 85, "Premium 1000-Piece Puzzle", null, 1, "TOYS-005", 249m, 249m, null },
                    { 86, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), 86, 86, "Watercolor Art Set 48pc", null, 1, "TOYS-006", 349m, 349m, null },
                    { 87, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), 87, 87, "Junior Science Kit", null, 1, "TOYS-007", 449m, 449m, null },
                    { 88, new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Utc), 88, 88, "Wooden Building Blocks 50pc", null, 1, "TOYS-008", 299m, 299m, null },
                    { 89, new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), 89, 89, "Deluxe Doll House", null, 1, "TOYS-009", 1299m, 1299m, null },
                    { 90, new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), 90, 90, "Elite Nerf Blaster Set", null, 1, "TOYS-010", 649m, 649m, null },
                    { 91, new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc), 91, 91, "Organic Fair Trade Coffee 500g", null, 1, "FOOD-001", 199m, 199m, null },
                    { 92, new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), 92, 92, "Premium Dark Chocolate Box", null, 1, "FOOD-002", 149m, 149m, null },
                    { 93, new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), 93, 93, "Raw Honey 1kg", null, 1, "FOOD-003", 249m, 249m, null },
                    { 94, new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), 94, 94, "Mixed Nuts and Seeds 500g", null, 1, "FOOD-004", 189m, 189m, null },
                    { 95, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), 95, 95, "Premium Green Tea 50 Bags", null, 1, "FOOD-005", 149m, 149m, null },
                    { 96, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), 96, 96, "Artisan Pasta Variety Box", null, 1, "FOOD-006", 249m, 249m, null },
                    { 97, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), 97, 97, "Extra Virgin Olive Oil 1L", null, 1, "FOOD-007", 199m, 199m, null },
                    { 98, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), 98, 98, "Granola Breakfast Mix 500g", null, 1, "FOOD-008", 169m, 169m, null },
                    { 99, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), 99, 99, "Protein Snack Bars 12-Pack", null, 1, "FOOD-009", 299m, 299m, null },
                    { 100, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Utc), 100, 100, "World Spice Collection Set", null, 1, "FOOD-010", 449m, 449m, null },
                    { 101, new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), 101, 1, "4K Smart TV 55\"", null, 1, "ELEC-001", 15999m, 15999m, null },
                    { 102, new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), 101, 51, "Premium Yoga Mat", null, 1, "SPRT-001", 399m, 399m, null },
                    { 103, new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), 102, 2, "Laptop Pro 15\"", null, 1, "ELEC-002", 22999m, 22999m, null },
                    { 104, new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), 102, 52, "Adjustable Dumbbell Set 20kg", null, 1, "SPRT-002", 1999m, 1999m, null },
                    { 105, new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), 103, 3, "Wireless Headphones", null, 1, "ELEC-003", 2499m, 2499m, null },
                    { 106, new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), 103, 53, "Trail Running Shoes", null, 1, "SPRT-003", 1299m, 1299m, null },
                    { 107, new DateTime(2024, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), 104, 4, "Smartphone X12", null, 1, "ELEC-004", 12999m, 12999m, null },
                    { 108, new DateTime(2024, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), 104, 54, "Sports Gym Bag 40L", null, 1, "SPRT-004", 549m, 549m, null },
                    { 109, new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), 105, 5, "Bluetooth Speaker", null, 1, "ELEC-005", 1299m, 1299m, null },
                    { 110, new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), 105, 55, "Resistance Bands Set 5pc", null, 1, "SPRT-005", 249m, 249m, null },
                    { 111, new DateTime(2024, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), 106, 6, "Gaming Console", null, 1, "ELEC-006", 9999m, 9999m, null },
                    { 112, new DateTime(2024, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), 106, 56, "Speed Jump Rope", null, 1, "SPRT-006", 149m, 149m, null },
                    { 113, new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), 107, 7, "Tablet 10\"", null, 1, "ELEC-007", 6999m, 6999m, null },
                    { 114, new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), 107, 57, "Cycling Safety Helmet", null, 1, "SPRT-007", 799m, 799m, null },
                    { 115, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc), 108, 8, "Smartwatch Series 5", null, 1, "ELEC-008", 3999m, 3999m, null },
                    { 116, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc), 108, 58, "Insulated Water Bottle 1L", null, 1, "SPRT-008", 299m, 299m, null },
                    { 117, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), 109, 9, "Wireless Earbuds", null, 1, "ELEC-009", 1999m, 1999m, null },
                    { 118, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), 109, 59, "Tennis Racket Pro", null, 1, "SPRT-009", 899m, 899m, null },
                    { 119, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), 110, 10, "Digital Camera 24MP", null, 1, "ELEC-010", 8999m, 8999m, null },
                    { 120, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), 110, 60, "Match Football Size 5", null, 1, "SPRT-010", 349m, 349m, null },
                    { 121, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), 111, 11, "USB-C Hub 7-in-1", null, 1, "ELEC-011", 599m, 599m, null },
                    { 122, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), 111, 61, "The Art of Clean Coding", null, 1, "BOOK-001", 349m, 349m, null },
                    { 123, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), 112, 12, "Mechanical Keyboard", null, 1, "ELEC-012", 1499m, 1499m, null },
                    { 124, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), 112, 62, "Business Strategy 101", null, 1, "BOOK-002", 299m, 299m, null },
                    { 125, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 113, 13, "Gaming Mouse", null, 1, "ELEC-013", 899m, 899m, null },
                    { 126, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 113, 63, "Mindfulness for Beginners", null, 1, "BOOK-003", 249m, 249m, null },
                    { 127, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 114, 14, "27\" Monitor", null, 1, "ELEC-014", 5999m, 5999m, null },
                    { 128, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 114, 64, "The Cooking Masterclass", null, 1, "BOOK-004", 449m, 449m, null },
                    { 129, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), 115, 15, "Portable Charger 20000mAh", null, 1, "ELEC-015", 499m, 499m, null },
                    { 130, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), 115, 65, "World History Encyclopedia", null, 1, "BOOK-005", 699m, 699m, null },
                    { 131, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), 116, 16, "Wi-Fi Router AC3000", null, 1, "ELEC-016", 1899m, 1899m, null },
                    { 132, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), 116, 66, "Science Fiction Anthology", null, 1, "BOOK-006", 199m, 199m, null },
                    { 133, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 117, 17, "NAS Drive 4TB", null, 1, "ELEC-017", 4999m, 4999m, null },
                    { 134, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 117, 67, "Children's Adventure Series", null, 1, "BOOK-007", 249m, 249m, null },
                    { 135, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), 118, 18, "VR Headset", null, 1, "ELEC-018", 7999m, 7999m, null },
                    { 136, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), 118, 68, "Photography Basics Guide", null, 1, "BOOK-008", 349m, 349m, null },
                    { 137, new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), 119, 19, "Drone with Camera", null, 1, "ELEC-019", 11999m, 11999m, null },
                    { 138, new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), 119, 69, "Financial Freedom Guide", null, 1, "BOOK-009", 299m, 299m, null },
                    { 139, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 120, 20, "Smart Home Hub", null, 1, "ELEC-020", 1299m, 1299m, null },
                    { 140, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 120, 70, "Language Learning Pack", null, 1, "BOOK-010", 549m, 549m, null },
                    { 141, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), 121, 21, "Men's Slim Fit Jeans", null, 1, "CLTH-001", 699m, 699m, null },
                    { 142, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), 121, 71, "Daily Multivitamin Complex", null, 1, "HLTH-001", 299m, 299m, null },
                    { 143, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), 122, 22, "Women's Summer Dress", null, 1, "CLTH-002", 599m, 599m, null },
                    { 144, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), 122, 72, "Whey Protein Powder 1kg", null, 1, "HLTH-002", 699m, 699m, null },
                    { 145, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), 123, 23, "Unisex Hoodie", null, 1, "CLTH-003", 499m, 499m, null },
                    { 146, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), 123, 73, "Face Moisturizer SPF30", null, 1, "HLTH-003", 349m, 349m, null },
                    { 147, new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), 124, 24, "Men's Business Shirt", null, 1, "CLTH-004", 449m, 449m, null },
                    { 148, new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), 124, 74, "Hair Growth Serum", null, 1, "HLTH-004", 449m, 449m, null },
                    { 149, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc), 125, 25, "Women's Yoga Leggings", null, 1, "CLTH-005", 399m, 399m, null },
                    { 150, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc), 125, 75, "Sonic Electric Toothbrush", null, 1, "HLTH-005", 799m, 799m, null },
                    { 151, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), 126, 26, "Men's Polo Shirt", null, 1, "CLTH-006", 349m, 349m, null },
                    { 152, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), 126, 76, "Digital Infrared Thermometer", null, 1, "HLTH-006", 299m, 299m, null },
                    { 153, new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc), 127, 27, "Women's Blazer", null, 1, "CLTH-007", 899m, 899m, null },
                    { 154, new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc), 127, 77, "Automatic Blood Pressure Monitor", null, 1, "HLTH-007", 999m, 999m, null },
                    { 155, new DateTime(2024, 8, 11, 0, 0, 0, 0, DateTimeKind.Utc), 128, 28, "Men's Chino Pants", null, 1, "CLTH-008", 599m, 599m, null },
                    { 156, new DateTime(2024, 8, 11, 0, 0, 0, 0, DateTimeKind.Utc), 128, 78, "Muscle Recovery Foam Roller", null, 1, "HLTH-008", 349m, 349m, null },
                    { 157, new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Utc), 129, 29, "Women's Denim Jacket", null, 1, "CLTH-009", 799m, 799m, null },
                    { 158, new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Utc), 129, 79, "Aromatherapy Diffuser", null, 1, "HLTH-009", 499m, 499m, null },
                    { 159, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), 130, 30, "Men's Cargo Shorts", null, 1, "CLTH-010", 449m, 449m, null },
                    { 160, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), 130, 80, "Melatonin Sleep Supplement", null, 1, "HLTH-010", 199m, 199m, null },
                    { 161, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), 131, 31, "Women's Cardigan", null, 1, "CLTH-011", 549m, 549m, null },
                    { 162, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), 131, 81, "LEGO City Police Station", null, 1, "TOYS-001", 1499m, 1499m, null },
                    { 163, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), 132, 32, "Men's V-Neck Sweater", null, 1, "CLTH-012", 499m, 499m, null },
                    { 164, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), 132, 82, "RC Off-Road Car 1:16", null, 1, "TOYS-002", 799m, 799m, null },
                    { 165, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), 133, 33, "Women's Mini Skirt", null, 1, "CLTH-013", 349m, 349m, null },
                    { 166, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), 133, 83, "Classic Monopoly Board Game", null, 1, "TOYS-003", 499m, 499m, null },
                    { 167, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), 134, 34, "Men's Tracksuit", null, 1, "CLTH-014", 799m, 799m, null },
                    { 168, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), 134, 84, "Giant Stuffed Teddy Bear", null, 1, "TOYS-004", 399m, 399m, null },
                    { 169, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), 135, 35, "Women's Windbreaker", null, 1, "CLTH-015", 699m, 699m, null },
                    { 170, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), 135, 85, "Premium 1000-Piece Puzzle", null, 1, "TOYS-005", 249m, 249m, null },
                    { 171, new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), 136, 36, "Coffee Table Oak", null, 1, "HOME-001", 2999m, 2999m, null },
                    { 172, new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), 136, 86, "Watercolor Art Set 48pc", null, 1, "TOYS-006", 349m, 349m, null },
                    { 173, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), 137, 37, "Queen Bed Frame", null, 1, "HOME-002", 4999m, 4999m, null },
                    { 174, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), 137, 87, "Junior Science Kit", null, 1, "TOYS-007", 449m, 449m, null },
                    { 175, new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), 138, 38, "Floor Lamp Modern", null, 1, "HOME-003", 799m, 799m, null },
                    { 176, new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), 138, 88, "Wooden Building Blocks 50pc", null, 1, "TOYS-008", 299m, 299m, null },
                    { 177, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Utc), 139, 39, "Wall Clock Minimalist", null, 1, "HOME-004", 349m, 349m, null },
                    { 178, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Utc), 139, 89, "Deluxe Doll House", null, 1, "TOYS-009", 1299m, 1299m, null },
                    { 179, new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Utc), 140, 40, "Throw Pillow Set 4pc", null, 1, "HOME-005", 299m, 299m, null },
                    { 180, new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Utc), 140, 90, "Elite Nerf Blaster Set", null, 1, "TOYS-010", 649m, 649m, null },
                    { 181, new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), 141, 41, "Kitchen Knife Set 7pc", null, 1, "HOME-006", 999m, 999m, null },
                    { 182, new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), 141, 91, "Organic Fair Trade Coffee 500g", null, 1, "FOOD-001", 199m, 199m, null },
                    { 183, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Utc), 142, 42, "Non-Stick Cookware Set", null, 1, "HOME-007", 1799m, 1799m, null },
                    { 184, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Utc), 142, 92, "Premium Dark Chocolate Box", null, 1, "FOOD-002", 149m, 149m, null },
                    { 185, new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc), 143, 43, "Garden Hose 30m", null, 1, "HOME-008", 399m, 399m, null },
                    { 186, new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc), 143, 93, "Raw Honey 1kg", null, 1, "FOOD-003", 249m, 249m, null },
                    { 187, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), 144, 44, "Ceramic Indoor Plant Pot", null, 1, "HOME-009", 199m, 199m, null },
                    { 188, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), 144, 94, "Mixed Nuts and Seeds 500g", null, 1, "FOOD-004", 189m, 189m, null },
                    { 189, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), 145, 45, "Blackout Curtain Set", null, 1, "HOME-010", 599m, 599m, null },
                    { 190, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), 145, 95, "Premium Green Tea 50 Bags", null, 1, "FOOD-005", 149m, 149m, null },
                    { 191, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), 146, 46, "Bath Towel Set 6pc", null, 1, "HOME-011", 449m, 449m, null },
                    { 192, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), 146, 96, "Artisan Pasta Variety Box", null, 1, "FOOD-006", 249m, 249m, null },
                    { 193, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), 147, 47, "Waterproof Shower Curtain", null, 1, "HOME-012", 249m, 249m, null },
                    { 194, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), 147, 97, "Extra Virgin Olive Oil 1L", null, 1, "FOOD-007", 199m, 199m, null },
                    { 195, new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), 148, 48, "Bamboo Desk Organizer", null, 1, "HOME-013", 299m, 299m, null },
                    { 196, new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), 148, 98, "Granola Breakfast Mix 500g", null, 1, "FOOD-008", 169m, 169m, null },
                    { 197, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc), 149, 49, "Bookshelf 5-Tier", null, 1, "HOME-014", 1499m, 1499m, null },
                    { 198, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc), 149, 99, "Protein Snack Bars 12-Pack", null, 1, "FOOD-009", 299m, 299m, null },
                    { 199, new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), 150, 50, "Non-Slip Doormat", null, 1, "HOME-015", 149m, 149m, null },
                    { 200, new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), 150, 100, "World Spice Collection Set", null, 1, "FOOD-010", 449m, 449m, null },
                    { 201, new DateTime(2024, 7, 13, 0, 0, 0, 0, DateTimeKind.Utc), 151, 51, "Premium Yoga Mat", null, 1, "SPRT-001", 399m, 399m, null },
                    { 202, new DateTime(2024, 7, 13, 0, 0, 0, 0, DateTimeKind.Utc), 151, 1, "4K Smart TV 55\"", null, 1, "ELEC-001", 15999m, 15999m, null },
                    { 203, new DateTime(2024, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 152, 52, "Adjustable Dumbbell Set 20kg", null, 1, "SPRT-002", 1999m, 1999m, null },
                    { 204, new DateTime(2024, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 152, 2, "Laptop Pro 15\"", null, 1, "ELEC-002", 22999m, 22999m, null },
                    { 205, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), 153, 53, "Trail Running Shoes", null, 1, "SPRT-003", 1299m, 1299m, null },
                    { 206, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), 153, 3, "Wireless Headphones", null, 1, "ELEC-003", 2499m, 2499m, null },
                    { 207, new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Utc), 154, 54, "Sports Gym Bag 40L", null, 1, "SPRT-004", 549m, 549m, null },
                    { 208, new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Utc), 154, 4, "Smartphone X12", null, 1, "ELEC-004", 12999m, 12999m, null },
                    { 209, new DateTime(2024, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc), 155, 55, "Resistance Bands Set 5pc", null, 1, "SPRT-005", 249m, 249m, null },
                    { 210, new DateTime(2024, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc), 155, 5, "Bluetooth Speaker", null, 1, "ELEC-005", 1299m, 1299m, null },
                    { 211, new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), 156, 56, "Speed Jump Rope", null, 1, "SPRT-006", 149m, 149m, null },
                    { 212, new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), 156, 6, "Gaming Console", null, 1, "ELEC-006", 9999m, 9999m, null },
                    { 213, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), 157, 57, "Cycling Safety Helmet", null, 1, "SPRT-007", 799m, 799m, null },
                    { 214, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), 157, 7, "Tablet 10\"", null, 1, "ELEC-007", 6999m, 6999m, null },
                    { 215, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), 158, 58, "Insulated Water Bottle 1L", null, 1, "SPRT-008", 299m, 299m, null },
                    { 216, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), 158, 8, "Smartwatch Series 5", null, 1, "ELEC-008", 3999m, 3999m, null },
                    { 217, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Utc), 159, 59, "Tennis Racket Pro", null, 1, "SPRT-009", 899m, 899m, null },
                    { 218, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Utc), 159, 9, "Wireless Earbuds", null, 1, "ELEC-009", 1999m, 1999m, null },
                    { 219, new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc), 160, 60, "Match Football Size 5", null, 1, "SPRT-010", 349m, 349m, null },
                    { 220, new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc), 160, 10, "Digital Camera 24MP", null, 1, "ELEC-010", 8999m, 8999m, null },
                    { 221, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), 161, 61, "The Art of Clean Coding", null, 1, "BOOK-001", 349m, 349m, null },
                    { 222, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), 161, 11, "USB-C Hub 7-in-1", null, 1, "ELEC-011", 599m, 599m, null },
                    { 223, new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), 162, 62, "Business Strategy 101", null, 1, "BOOK-002", 299m, 299m, null },
                    { 224, new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), 162, 12, "Mechanical Keyboard", null, 1, "ELEC-012", 1499m, 1499m, null },
                    { 225, new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Utc), 163, 63, "Mindfulness for Beginners", null, 1, "BOOK-003", 249m, 249m, null },
                    { 226, new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Utc), 163, 13, "Gaming Mouse", null, 1, "ELEC-013", 899m, 899m, null },
                    { 227, new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc), 164, 64, "The Cooking Masterclass", null, 1, "BOOK-004", 449m, 449m, null },
                    { 228, new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc), 164, 14, "27\" Monitor", null, 1, "ELEC-014", 5999m, 5999m, null },
                    { 229, new DateTime(2024, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), 165, 65, "World History Encyclopedia", null, 1, "BOOK-005", 699m, 699m, null },
                    { 230, new DateTime(2024, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), 165, 15, "Portable Charger 20000mAh", null, 1, "ELEC-015", 499m, 499m, null },
                    { 231, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), 166, 66, "Science Fiction Anthology", null, 1, "BOOK-006", 199m, 199m, null },
                    { 232, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), 166, 16, "Wi-Fi Router AC3000", null, 1, "ELEC-016", 1899m, 1899m, null },
                    { 233, new DateTime(2024, 11, 14, 0, 0, 0, 0, DateTimeKind.Utc), 167, 67, "Children's Adventure Series", null, 1, "BOOK-007", 249m, 249m, null },
                    { 234, new DateTime(2024, 11, 14, 0, 0, 0, 0, DateTimeKind.Utc), 167, 17, "NAS Drive 4TB", null, 1, "ELEC-017", 4999m, 4999m, null },
                    { 235, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), 168, 68, "Photography Basics Guide", null, 1, "BOOK-008", 349m, 349m, null },
                    { 236, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), 168, 18, "VR Headset", null, 1, "ELEC-018", 7999m, 7999m, null },
                    { 237, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 169, 69, "Financial Freedom Guide", null, 1, "BOOK-009", 299m, 299m, null },
                    { 238, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 169, 19, "Drone with Camera", null, 1, "ELEC-019", 11999m, 11999m, null },
                    { 239, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), 170, 70, "Language Learning Pack", null, 1, "BOOK-010", 549m, 549m, null },
                    { 240, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), 170, 20, "Smart Home Hub", null, 1, "ELEC-020", 1299m, 1299m, null },
                    { 241, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 171, 71, "Daily Multivitamin Complex", null, 1, "HLTH-001", 299m, 299m, null },
                    { 242, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 171, 21, "Men's Slim Fit Jeans", null, 1, "CLTH-001", 699m, 699m, null },
                    { 243, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), 172, 72, "Whey Protein Powder 1kg", null, 1, "HLTH-002", 699m, 699m, null },
                    { 244, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), 172, 22, "Women's Summer Dress", null, 1, "CLTH-002", 599m, 599m, null },
                    { 245, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 173, 73, "Face Moisturizer SPF30", null, 1, "HLTH-003", 349m, 349m, null },
                    { 246, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 173, 23, "Unisex Hoodie", null, 1, "CLTH-003", 499m, 499m, null },
                    { 247, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), 174, 74, "Hair Growth Serum", null, 1, "HLTH-004", 449m, 449m, null },
                    { 248, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), 174, 24, "Men's Business Shirt", null, 1, "CLTH-004", 449m, 449m, null },
                    { 249, new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), 175, 75, "Sonic Electric Toothbrush", null, 1, "HLTH-005", 799m, 799m, null },
                    { 250, new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), 175, 25, "Women's Yoga Leggings", null, 1, "CLTH-005", 399m, 399m, null },
                    { 251, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), 176, 76, "Digital Infrared Thermometer", null, 1, "HLTH-006", 299m, 299m, null },
                    { 252, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), 176, 26, "Men's Polo Shirt", null, 1, "CLTH-006", 349m, 349m, null },
                    { 253, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), 177, 77, "Automatic Blood Pressure Monitor", null, 1, "HLTH-007", 999m, 999m, null },
                    { 254, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), 177, 27, "Women's Blazer", null, 1, "CLTH-007", 899m, 899m, null },
                    { 255, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), 178, 78, "Muscle Recovery Foam Roller", null, 1, "HLTH-008", 349m, 349m, null },
                    { 256, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), 178, 28, "Men's Chino Pants", null, 1, "CLTH-008", 599m, 599m, null },
                    { 257, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Utc), 179, 79, "Aromatherapy Diffuser", null, 1, "HLTH-009", 499m, 499m, null },
                    { 258, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Utc), 179, 29, "Women's Denim Jacket", null, 1, "CLTH-009", 799m, 799m, null },
                    { 259, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 180, 80, "Melatonin Sleep Supplement", null, 1, "HLTH-010", 199m, 199m, null },
                    { 260, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 180, 30, "Men's Cargo Shorts", null, 1, "CLTH-010", 449m, 449m, null },
                    { 261, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), 181, 81, "LEGO City Police Station", null, 1, "TOYS-001", 1499m, 1499m, null },
                    { 262, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), 181, 31, "Women's Cardigan", null, 1, "CLTH-011", 549m, 549m, null },
                    { 263, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), 182, 82, "RC Off-Road Car 1:16", null, 1, "TOYS-002", 799m, 799m, null },
                    { 264, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), 182, 32, "Men's V-Neck Sweater", null, 1, "CLTH-012", 499m, 499m, null },
                    { 265, new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Utc), 183, 83, "Classic Monopoly Board Game", null, 1, "TOYS-003", 499m, 499m, null },
                    { 266, new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Utc), 183, 33, "Women's Mini Skirt", null, 1, "CLTH-013", 349m, 349m, null },
                    { 267, new DateTime(2024, 4, 16, 0, 0, 0, 0, DateTimeKind.Utc), 184, 84, "Giant Stuffed Teddy Bear", null, 1, "TOYS-004", 399m, 399m, null },
                    { 268, new DateTime(2024, 4, 16, 0, 0, 0, 0, DateTimeKind.Utc), 184, 34, "Men's Tracksuit", null, 1, "CLTH-014", 799m, 799m, null },
                    { 269, new DateTime(2024, 5, 16, 0, 0, 0, 0, DateTimeKind.Utc), 185, 85, "Premium 1000-Piece Puzzle", null, 1, "TOYS-005", 249m, 249m, null },
                    { 270, new DateTime(2024, 5, 16, 0, 0, 0, 0, DateTimeKind.Utc), 185, 35, "Women's Windbreaker", null, 1, "CLTH-015", 699m, 699m, null },
                    { 271, new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), 186, 86, "Watercolor Art Set 48pc", null, 1, "TOYS-006", 349m, 349m, null },
                    { 272, new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), 186, 36, "Coffee Table Oak", null, 1, "HOME-001", 2999m, 2999m, null },
                    { 273, new DateTime(2024, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc), 187, 87, "Junior Science Kit", null, 1, "TOYS-007", 449m, 449m, null },
                    { 274, new DateTime(2024, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc), 187, 37, "Queen Bed Frame", null, 1, "HOME-002", 4999m, 4999m, null },
                    { 275, new DateTime(2024, 8, 16, 0, 0, 0, 0, DateTimeKind.Utc), 188, 88, "Wooden Building Blocks 50pc", null, 1, "TOYS-008", 299m, 299m, null },
                    { 276, new DateTime(2024, 8, 16, 0, 0, 0, 0, DateTimeKind.Utc), 188, 38, "Floor Lamp Modern", null, 1, "HOME-003", 799m, 799m, null },
                    { 277, new DateTime(2024, 9, 16, 0, 0, 0, 0, DateTimeKind.Utc), 189, 89, "Deluxe Doll House", null, 1, "TOYS-009", 1299m, 1299m, null },
                    { 278, new DateTime(2024, 9, 16, 0, 0, 0, 0, DateTimeKind.Utc), 189, 39, "Wall Clock Minimalist", null, 1, "HOME-004", 349m, 349m, null },
                    { 279, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Utc), 190, 90, "Elite Nerf Blaster Set", null, 1, "TOYS-010", 649m, 649m, null },
                    { 280, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Utc), 190, 40, "Throw Pillow Set 4pc", null, 1, "HOME-005", 299m, 299m, null },
                    { 281, new DateTime(2024, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc), 191, 91, "Organic Fair Trade Coffee 500g", null, 1, "FOOD-001", 199m, 199m, null },
                    { 282, new DateTime(2024, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc), 191, 41, "Kitchen Knife Set 7pc", null, 1, "HOME-006", 999m, 999m, null },
                    { 283, new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), 192, 92, "Premium Dark Chocolate Box", null, 1, "FOOD-002", 149m, 149m, null },
                    { 284, new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), 192, 42, "Non-Stick Cookware Set", null, 1, "HOME-007", 1799m, 1799m, null },
                    { 285, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), 193, 93, "Raw Honey 1kg", null, 1, "FOOD-003", 249m, 249m, null },
                    { 286, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), 193, 43, "Garden Hose 30m", null, 1, "HOME-008", 399m, 399m, null },
                    { 287, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), 194, 94, "Mixed Nuts and Seeds 500g", null, 1, "FOOD-004", 189m, 189m, null },
                    { 288, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), 194, 44, "Ceramic Indoor Plant Pot", null, 1, "HOME-009", 199m, 199m, null },
                    { 289, new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Utc), 195, 95, "Premium Green Tea 50 Bags", null, 1, "FOOD-005", 149m, 149m, null },
                    { 290, new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Utc), 195, 45, "Blackout Curtain Set", null, 1, "HOME-010", 599m, 599m, null },
                    { 291, new DateTime(2024, 4, 17, 0, 0, 0, 0, DateTimeKind.Utc), 196, 96, "Artisan Pasta Variety Box", null, 1, "FOOD-006", 249m, 249m, null },
                    { 292, new DateTime(2024, 4, 17, 0, 0, 0, 0, DateTimeKind.Utc), 196, 46, "Bath Towel Set 6pc", null, 1, "HOME-011", 449m, 449m, null },
                    { 293, new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Utc), 197, 97, "Extra Virgin Olive Oil 1L", null, 1, "FOOD-007", 199m, 199m, null },
                    { 294, new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Utc), 197, 47, "Waterproof Shower Curtain", null, 1, "HOME-012", 249m, 249m, null },
                    { 295, new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), 198, 98, "Granola Breakfast Mix 500g", null, 1, "FOOD-008", 169m, 169m, null },
                    { 296, new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), 198, 48, "Bamboo Desk Organizer", null, 1, "HOME-013", 299m, 299m, null },
                    { 297, new DateTime(2024, 7, 17, 0, 0, 0, 0, DateTimeKind.Utc), 199, 99, "Protein Snack Bars 12-Pack", null, 1, "FOOD-009", 299m, 299m, null },
                    { 298, new DateTime(2024, 7, 17, 0, 0, 0, 0, DateTimeKind.Utc), 199, 49, "Bookshelf 5-Tier", null, 1, "HOME-014", 1499m, 1499m, null },
                    { 299, new DateTime(2024, 8, 17, 0, 0, 0, 0, DateTimeKind.Utc), 200, 100, "World Spice Collection Set", null, 1, "FOOD-010", 449m, 449m, null },
                    { 300, new DateTime(2024, 8, 17, 0, 0, 0, 0, DateTimeKind.Utc), 200, 50, "Non-Slip Doormat", null, 1, "HOME-015", 149m, 149m, null }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "CardBrand", "CardLastFour", "CreatedAt", "Currency", "FailureReason", "Method", "OrderId", "PaidAt", "Status", "TransactionId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 18477.85m, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 26527.85m, null, null, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 2, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 2952.85m, null, null, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 3, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 15027.85m, null, null, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 4, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 1572.85m, null, null, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 5, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 11577.85m, null, null, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 6, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 8127.85m, null, null, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 7, new DateTime(2024, 7, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 4677.85m, null, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 8, new DateTime(2024, 8, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 2377.85m, null, null, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 9, new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 10427.85m, null, null, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 10, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 767.85m, null, null, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 11, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 1802.85m, null, null, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 12, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 1112.85m, null, null, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 13, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 6977.85m, null, null, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 14, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 652.85m, null, null, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 15, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, 2262.85m, null, null, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 16, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, 5827.85m, null, null, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 17, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, 9277.85m, null, null, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 18, new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, 13877.85m, null, null, new DateTime(2024, 7, 2, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 19, new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, 1572.85m, null, null, new DateTime(2024, 8, 2, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 20, new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 21, 882.85m, null, null, new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 21, new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 22, 767.85m, null, null, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 22, new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, 652.85m, null, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 23, new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 24, 595.35m, null, null, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 24, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 25, 537.85m, null, null, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 25, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 26, 480.35m, null, null, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 26, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27, 1112.85m, null, null, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 27, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 28, 767.85m, null, null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 28, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 29, 997.85m, null, null, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 29, new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 30, 595.35m, null, null, new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 30, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 31, 710.35m, null, null, new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 31, new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 32, 652.85m, null, null, new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 32, new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 33, 480.35m, null, null, new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 33, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 34, 997.85m, null, null, new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 34, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 35, 882.85m, null, null, new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 35, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 36, 3527.85m, null, null, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 36, new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 37, 5827.85m, null, null, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 37, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 38, 997.85m, null, null, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 38, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 39, 480.35m, null, null, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 39, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 40, 422.85m, null, null, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 40, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 41, 1227.85m, null, null, new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 41, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 42, 2147.85m, null, null, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 42, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 43, 537.85m, null, null, new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 43, new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 44, 307.85m, null, null, new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 44, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 45, 767.85m, null, null, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 45, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 46, 595.35m, null, null, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 46, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 47, 365.35m, null, null, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 47, new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 48, 422.85m, null, null, new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 48, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 49, 1802.85m, null, null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 49, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 50, 250.35m, null, null, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 50, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 51, 537.85m, null, null, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 51, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 52, 2377.85m, null, null, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 52, new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 53, 1572.85m, null, null, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 53, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 54, 710.35m, null, null, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 54, new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 55, 365.35m, null, null, new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 55, new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 56, 250.35m, null, null, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 56, new DateTime(2024, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 57, 997.85m, null, null, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 57, new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 58, 422.85m, null, null, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 58, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 59, 1112.85m, null, null, new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 59, new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 60, 480.35m, null, null, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 60, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 61, 480.35m, null, null, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 61, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 62, 422.85m, null, null, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 62, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 63, 365.35m, null, null, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 63, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 64, 595.35m, null, null, new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 64, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 65, 882.85m, null, null, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 65, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 66, 307.85m, null, null, new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 66, new DateTime(2024, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 67, 365.35m, null, null, new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 67, new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 68, 480.35m, null, null, new DateTime(2024, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 68, new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 69, 422.85m, null, null, new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 69, new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 70, 710.35m, null, null, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 70, new DateTime(2024, 10, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 71, 422.85m, null, null, new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 71, new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 72, 882.85m, null, null, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 72, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 73, 480.35m, null, null, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 73, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 74, 595.35m, null, null, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 74, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 75, 997.85m, null, null, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 75, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 76, 422.85m, null, null, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 76, new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 77, 1227.85m, null, null, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 77, new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 78, 480.35m, null, null, new DateTime(2024, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 78, new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 79, 652.85m, null, null, new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 79, new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 80, 307.85m, null, null, new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 80, new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 81, 1802.85m, null, null, new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 81, new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 82, 997.85m, null, null, new DateTime(2024, 10, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 82, new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 83, 652.85m, null, null, new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 83, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 84, 537.85m, null, null, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 84, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 85, 365.35m, null, null, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 85, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 86, 480.35m, null, null, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 86, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 87, 595.35m, null, null, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 87, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 88, 422.85m, null, null, new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 88, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 89, 1572.85m, null, null, new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 89, new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 90, 825.35m, null, null, new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 90, new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 91, 307.85m, null, null, new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 91, new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 92, 250.35m, null, null, new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 92, new DateTime(2024, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 93, 365.35m, null, null, new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 93, new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 94, 296.35m, null, null, new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 94, new DateTime(2024, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 95, 250.35m, null, null, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 95, new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 96, 365.35m, null, null, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 96, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 97, 307.85m, null, null, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 97, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 98, 273.35m, null, null, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 98, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 99, 422.85m, null, null, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 99, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 100, 595.35m, null, null, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 100, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 101, 18936.70m, null, null, new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 101, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 102, 28826.70m, null, null, new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 102, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 103, 4446.70m, null, null, new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 103, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 104, 15659.20m, null, null, new DateTime(2024, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 104, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 105, 1859.20m, null, null, new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 105, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 106, 11749.20m, null, null, new DateTime(2024, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 106, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 107, 9046.70m, null, null, new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 107, new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 108, 5021.70m, null, null, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 108, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 109, 3411.70m, null, null, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 109, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 110, 10829.20m, null, null, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 110, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 111, 1169.20m, null, null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 111, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 112, 2146.70m, null, null, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 112, new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 113, 1399.20m, null, null, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 113, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 114, 7494.20m, null, null, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 114, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 115, 1456.70m, null, null, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 115, new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 116, 2491.70m, null, null, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 116, new DateTime(2024, 8, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 117, 6114.20m, null, null, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 117, new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 118, 9679.20m, null, null, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 118, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 119, 14221.70m, null, null, new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 119, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 120, 2204.20m, null, null, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 120, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 121, 1226.70m, null, null, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 121, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 122, 1571.70m, null, null, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 122, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 123, 1054.20m, null, null, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 123, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 124, 1111.70m, null, null, new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 124, new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 125, 1456.70m, null, null, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 125, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 126, 824.20m, null, null, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 126, new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 127, 2261.70m, null, null, new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 127, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 128, 1169.20m, null, null, new DateTime(2024, 8, 11, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 128, new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 129, 1571.70m, null, null, new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 129, new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 130, 824.20m, null, null, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 130, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 131, 2434.20m, null, null, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 131, new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 132, 1571.70m, null, null, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 132, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 133, 1054.20m, null, null, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 133, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 134, 1456.70m, null, null, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 134, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 135, 1169.20m, null, null, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 135, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 136, 3929.20m, null, null, new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 136, new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 137, 6344.20m, null, null, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 137, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 138, 1341.70m, null, null, new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 138, new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 139, 1974.20m, null, null, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 139, new DateTime(2024, 7, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 140, 1169.20m, null, null, new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 140, new DateTime(2024, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 141, 1456.70m, null, null, new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 141, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 142, 2319.20m, null, null, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 142, new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 143, 824.20m, null, null, new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 143, new DateTime(2024, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 144, 525.20m, null, null, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 144, new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 145, 939.20m, null, null, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 145, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 146, 881.70m, null, null, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 146, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 147, 594.20m, null, null, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 147, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 148, 617.20m, null, null, new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 148, new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 149, 2146.70m, null, null, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 149, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 150, 766.70m, null, null, new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 150, new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 151, 18936.70m, null, null, new DateTime(2024, 7, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 151, new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 7, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 152, 28826.70m, null, null, new DateTime(2024, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 152, new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 153, 4446.70m, null, null, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 153, new DateTime(2024, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 154, 15659.20m, null, null, new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 154, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 155, 1859.20m, null, null, new DateTime(2024, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 155, new DateTime(2024, 11, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 156, 11749.20m, null, null, new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 156, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 157, 9046.70m, null, null, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 157, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 158, 5021.70m, null, null, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 158, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 159, 3411.70m, null, null, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 159, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 160, 10829.20m, null, null, new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc), "ZAR", null, "MockCard", 160, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", null, new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSettings_Key",
                table: "AppSettings",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductVariantId",
                table: "CartItems",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Slug",
                table: "Categories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_Code",
                table: "Coupons",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLogs_ChangedByUserId",
                table: "InventoryLogs",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLogs_ProductId",
                table: "InventoryLogs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLogs_ProductVariantId",
                table: "InventoryLogs",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductVariantId",
                table: "OrderItems",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponId",
                table: "Orders",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistories_ChangedByUserId",
                table: "OrderStatusHistories",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistories_OrderId",
                table: "OrderStatusHistories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductId",
                table: "ProductAttributes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_OrderId",
                table: "ProductReviews",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId_UserId_OrderId",
                table: "ProductReviews",
                columns: new[] { "ProductId", "UserId", "OrderId" },
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_UserId",
                table: "ProductReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SKU",
                table: "Products",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Slug",
                table: "Products",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_SKU",
                table: "ProductVariants",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ProductId",
                table: "Wishlists",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId_ProductId",
                table: "Wishlists",
                columns: new[] { "UserId", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BannerSlides");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "InventoryLogs");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderStatusHistories");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "ShippingMethods");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
