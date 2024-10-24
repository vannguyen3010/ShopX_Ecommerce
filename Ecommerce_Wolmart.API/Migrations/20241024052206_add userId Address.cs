using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class adduserIdAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "74d1495c-cd63-4853-b58e-f451ddd1c4f5", "AQAAAAIAAYagAAAAEJF0vnHkUsBY/NHHzUzP9H6Xi16KpcANtZLW4C0NfekHWmhp8SAv5JrrD0wVzXpxSw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ab559399-eec7-4818-819d-7ff21fad5d61", "AQAAAAIAAYagAAAAELnQsTStzJYF/KdX0hhnq8cKQ6A39UvuNhEKowmMJ/GIbP6kZZk0gVHLlF91R+MYXw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Addresses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ce7c6943-3e34-4c64-81b3-521fb8bf72fa", "AQAAAAIAAYagAAAAEGFCBCJNUCChfkPATmo1/FCXf+TP8BuNNijqP/sy9YQ/TV68H8uH3LMy/JtAnRy3Xg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e39efbdf-9083-47ec-9381-8a371edf46ca", "AQAAAAIAAYagAAAAECFBkZtNKukkncHGHTldSIq53xJ5C6oKAzS5B7joMoqoJ5RCN73YGF5mHtw9fFyk/w==" });
        }
    }
}
