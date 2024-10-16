using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "db140bc5-dc5a-4cd8-8a36-047e80787320", "AQAAAAIAAYagAAAAEO8OYZSANjU7geHV3xKDWTSx0zQTdhypXXM4+/6wY95fbuj/d7dGdHn+2vbTJ9VZGQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "747e58df-c76c-48ea-9b28-300c40b73228", "AQAAAAIAAYagAAAAEOpF7PPVgU3MuSORjBpeBN19ZGZUcbsW59+l3MiVOZpUAHcOF2tKKr7820g9oe03nQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Addresses");

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
    }
}
