using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopForge.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                column: "PasswordHash",
                value: "$2a$04$eNtEILhcMwDSjKuiPHNnmeO/W9ch/.0UqsHpiw/Zw.2hbOmGa9GPS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$04$eNtEILhcMwDSjKuiPHNnmeO/W9ch/.0UqsHpiw/Zw.2hbOmGa9GPS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "$2a$04$eNtEILhcMwDSjKuiPHNnmeO/W9ch/.0UqsHpiw/Zw.2hbOmGa9GPS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "$2a$04$eNtEILhcMwDSjKuiPHNnmeO/W9ch/.0UqsHpiw/Zw.2hbOmGa9GPS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "$2a$04$eNtEILhcMwDSjKuiPHNnmeO/W9ch/.0UqsHpiw/Zw.2hbOmGa9GPS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$04$eGWwwzjZU8ow6Uq7JfH5wusVobYEYHCiFG.2OUCjrDME9jdF3F/bG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$04$QE50Atk1kWG8NVoEWrQ9outpgZ/L7CPMO9W/D5eyT8UPg5vrFOFd6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$04$mztCitgMJViuo3FpY4pI3OJPNTtQRKzEo5ont6NgLWQbdc.X7ZTZi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$04$mztCitgMJViuo3FpY4pI3OJPNTtQRKzEo5ont6NgLWQbdc.X7ZTZi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "$2a$04$mztCitgMJViuo3FpY4pI3OJPNTtQRKzEo5ont6NgLWQbdc.X7ZTZi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "$2a$04$mztCitgMJViuo3FpY4pI3OJPNTtQRKzEo5ont6NgLWQbdc.X7ZTZi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "$2a$04$mztCitgMJViuo3FpY4pI3OJPNTtQRKzEo5ont6NgLWQbdc.X7ZTZi");
        }
    }
}
