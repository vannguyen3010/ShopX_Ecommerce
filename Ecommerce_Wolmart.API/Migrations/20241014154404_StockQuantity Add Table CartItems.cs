using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class StockQuantityAddTableCartItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "58094d00-6572-44b4-b77b-b3cb12932593", "AQAAAAIAAYagAAAAENATQ6pS1dxZth+Roy+9jnnuJiWUtSAVg/qqIceF3VjJFTUeHoUkZQ0leaME3r6n/g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8d6bfe48-73cc-456a-92f6-478549eb266c", "AQAAAAIAAYagAAAAEHHecY9AbmsvYLkNPb7Bxim4UchPq+zGBVpVr7jBNVfQX8HbcD5qtdPEhWjfRcx/ug==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "CartItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2852668b-f9aa-41a8-bc8d-05f743f70691", "AQAAAAIAAYagAAAAELtpEHGziSPoO26CxER1pZhONc9QOvelDLXH3e2poF9AwjgbAbqfZ3YcuiHFWJfJkg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ccddd426-887e-49ca-b243-11e75c8c87d0", "AQAAAAIAAYagAAAAEOGX4PPsHUwxC0meEvyatmAD7bPWK+IcX3HjeeEg0N1oPTmcEKk4V7y6YpYx5rTDPQ==" });
        }
    }
}
