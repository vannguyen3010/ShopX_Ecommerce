using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableCartItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "CartItems",
                newName: "NameSlug");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "NameSlug",
                table: "CartItems",
                newName: "ProductName");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "37a665d5-b6a5-4b4c-8981-8a1d9853a3f7", "AQAAAAIAAYagAAAAEGl/XqDmpapZ4VuQ2FoCUT7tYtA1DbU7spndyKW+n2E9GDIjxLWf6y+gpxuOOBleyw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0d7eafea-cbce-4e28-9b10-1676e141e245", "AQAAAAIAAYagAAAAEPTeRqpjMECtoW2604sv/XKzBm5CaSKhT4jdUdgm/mtBbe40PpIfWe+c1uHddPrkog==" });
        }
    }
}
