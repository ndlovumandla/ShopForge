using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopForge.Database.Migrations
{
    /// <inheritdoc />
    public partial class DummyDataRefresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "Customer003 Demo");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2,
                column: "FullName",
                value: "Customer004 Demo");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 3,
                column: "FullName",
                value: "Customer005 Demo");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 4,
                column: "FullName",
                value: "Customer006 Demo");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 5,
                column: "FullName",
                value: "Customer007 Demo");

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Description", "DiscountValue", "ExpiresAt", "MinimumOrderAmount", "UsageLimit" },
                values: new object[] { "DUMMY01", "Demo coupon 01", 6m, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 100m, 100 });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Description", "DiscountType", "DiscountValue", "ExpiresAt", "MinimumOrderAmount", "UsageLimit" },
                values: new object[] { "DUMMY02", "Demo coupon 02", "Percentage", 7m, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 200m, 200 });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "Description", "DiscountType", "DiscountValue", "ExpiresAt", "UsageLimit" },
                values: new object[] { "DUMMY03", "Demo coupon 03", "FixedAmount", 60m, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 300 });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "DiscountType", "DiscountValue", "ExpiresAt", "IsActive", "MaximumDiscountAmount", "MinimumOrderAmount", "StartsAt", "UpdatedAt", "UsageCount", "UsageLimit" },
                values: new object[,]
                {
                    { 4, "DUMMY04", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Demo coupon 04", "Percentage", 9m, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 400m, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, 400 },
                    { 5, "DUMMY05", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Demo coupon 05", "Percentage", 10m, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 500m, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, 500 },
                    { 6, "DUMMY06", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Demo coupon 06", "FixedAmount", 120m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 600m, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, 600 },
                    { 7, "DUMMY07", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Demo coupon 07", "Percentage", 12m, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 700m, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, 700 },
                    { 8, "DUMMY08", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Demo coupon 08", "Percentage", 13m, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 800m, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, 800 },
                    { 9, "DUMMY09", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Demo coupon 09", "FixedAmount", 180m, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 900m, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, 900 },
                    { 10, "DUMMY10", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Demo coupon 10", "Percentage", 15m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 1000m, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, 1000 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$04$wXDsvMYI.v1TZOKubnNAAONoq5/KQYXlwtBUU41li8HGe1KxcdquG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$04$PfxTALNSprxdGj5Aqi7DVu0mqG2nDXQgOdsxTGcDGHK0veQCx8Eom");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "customer003@example.com", "Customer003", "Demo", "$2a$04$HH4UnPhgPuGhxaKYVCQFR.qPEosplC8yFqdCFhni79CfL.wzlrgpu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "customer004@example.com", "Customer004", "Demo", "$2a$04$HH4UnPhgPuGhxaKYVCQFR.qPEosplC8yFqdCFhni79CfL.wzlrgpu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "customer005@example.com", "Customer005", "Demo", "$2a$04$HH4UnPhgPuGhxaKYVCQFR.qPEosplC8yFqdCFhni79CfL.wzlrgpu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "customer006@example.com", "Customer006", "Demo", "$2a$04$HH4UnPhgPuGhxaKYVCQFR.qPEosplC8yFqdCFhni79CfL.wzlrgpu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "customer007@example.com", "Customer007", "Demo", "$2a$04$HH4UnPhgPuGhxaKYVCQFR.qPEosplC8yFqdCFhni79CfL.wzlrgpu" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "EmailVerified", "FirstName", "IsActive", "LastLoginAt", "LastName", "PasswordHash", "PhoneNumber", "ProfileImageUrl", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer008@example.com", true, "Customer008", true, null, "Demo", "$2a$04$HH4UnPhgPuGhxaKYVCQFR.qPEosplC8yFqdCFhni79CfL.wzlrgpu", null, null, "Customer", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer009@example.com", true, "Customer009", true, null, "Demo", "$2a$04$HH4UnPhgPuGhxaKYVCQFR.qPEosplC8yFqdCFhni79CfL.wzlrgpu", null, null, "Customer", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer010@example.com", true, "Customer010", true, null, "Demo", "$2a$04$HH4UnPhgPuGhxaKYVCQFR.qPEosplC8yFqdCFhni79CfL.wzlrgpu", null, null, "Customer", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer011@example.com", true, "Customer011", true, null, "Demo", "$2a$04$HH4UnPhgPuGhxaKYVCQFR.qPEosplC8yFqdCFhni79CfL.wzlrgpu", null, null, "Customer", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer012@example.com", true, "Customer012", true, null, "Demo", "$2a$04$HH4UnPhgPuGhxaKYVCQFR.qPEosplC8yFqdCFhni79CfL.wzlrgpu", null, null, "Customer", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer013@example.com", true, "Customer013", true, null, "Demo", "$2a$04$HH4UnPhgPuGhxaKYVCQFR.qPEosplC8yFqdCFhni79CfL.wzlrgpu", null, null, "Customer", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer014@example.com", true, "Customer014", true, null, "Demo", "$2a$04$HH4UnPhgPuGhxaKYVCQFR.qPEosplC8yFqdCFhni79CfL.wzlrgpu", null, null, "Customer", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "John Doe");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2,
                column: "FullName",
                value: "Jane Smith");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 3,
                column: "FullName",
                value: "Bob Jones");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 4,
                column: "FullName",
                value: "Alice Brown");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 5,
                column: "FullName",
                value: "Charlie Davis");

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Description", "DiscountValue", "ExpiresAt", "MinimumOrderAmount", "UsageLimit" },
                values: new object[] { "WELCOME10", "10% off your first order", 10m, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), null, 1000 });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Description", "DiscountType", "DiscountValue", "ExpiresAt", "MinimumOrderAmount", "UsageLimit" },
                values: new object[] { "SAVE50", "R50 off orders over R500", "FixedAmount", 50m, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), 500m, 500 });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "Description", "DiscountType", "DiscountValue", "ExpiresAt", "UsageLimit" },
                values: new object[] { "FREESHIP", "Free shipping on orders over R300", "FreeShipping", 0m, null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$04$edoLpfz4ekEnRKpS8N4HOea8oZ3zu508w9CNsrlic71cxEjfJ5BAC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$04$QdbJtZbobelcQAuTUrJZ/uNeTl1BRLwsBHJlSK.Qfhq1RivRBTTZq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "john.doe@example.com", "John", "Doe", "$2a$04$eNtEILhcMwDSjKuiPHNnmeO/W9ch/.0UqsHpiw/Zw.2hbOmGa9GPS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "jane.smith@example.com", "Jane", "Smith", "$2a$04$eNtEILhcMwDSjKuiPHNnmeO/W9ch/.0UqsHpiw/Zw.2hbOmGa9GPS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "bob.jones@example.com", "Bob", "Jones", "$2a$04$eNtEILhcMwDSjKuiPHNnmeO/W9ch/.0UqsHpiw/Zw.2hbOmGa9GPS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "alice.brown@example.com", "Alice", "Brown", "$2a$04$eNtEILhcMwDSjKuiPHNnmeO/W9ch/.0UqsHpiw/Zw.2hbOmGa9GPS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "charlie.davis@example.com", "Charlie", "Davis", "$2a$04$eNtEILhcMwDSjKuiPHNnmeO/W9ch/.0UqsHpiw/Zw.2hbOmGa9GPS" });
        }
    }
}
