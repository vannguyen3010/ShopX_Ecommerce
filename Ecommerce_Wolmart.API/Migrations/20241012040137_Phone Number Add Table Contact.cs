using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class PhoneNumberAddTableContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Contacts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4c2d2bac-06e1-4ca0-bcf1-b04d37846c07", "AQAAAAIAAYagAAAAEDX1AtXpSS8rQEeoZaahawT9476H0O19YgbmEyXgrJNLkYVBfCpH6MRwW91y3tz5nQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1bf4632a-81c5-4296-ab5c-f2c70e775e94", "AQAAAAIAAYagAAAAEFViXWkHgRGewqoFFS5y3jtuOo6ESvJspBTY0GK/Yg9Gs0Fm2qz+xMWRR6NLEmsPVQ==" });
        }
    }
}
