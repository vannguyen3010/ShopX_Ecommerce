using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class stringconvertboolOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "65d05f41-10d2-4629-850e-efb599bd614c", "AQAAAAIAAYagAAAAEBbqZKQh0L/SYJbUt4hWW+Y71G1Cbqui3NHKUmqhJ8/rvHeF5qQcKmk6etITdVQ7Qw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9d6d3d7b-a4f2-4a65-8b8c-90aa89c2654c", "AQAAAAIAAYagAAAAEKH1Hak+TCEWl4WYG+RDfyUkoRuzUR/SjyKB6xbeMN8vu/YsCfZ1+uFWWDPg3MzHbQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "OrderStatus",
                table: "Orders",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fd39c1ba-c2ed-4a3e-a045-174a236b2942", "AQAAAAIAAYagAAAAEPg6Gw9CiOuCCqdtTuI2vzoRvqVl0pHefa3Zt+tDLAvkfmHhgZZy399tnGViimjE6A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b337d3cd-0a5f-40c6-ac17-3ad9a40b5f00", "AQAAAAIAAYagAAAAENWp84joTbjtw4RpFrxuwOGpQ8zkZRRU7d5TMtNfZgfXuWc8s4rKWBtfhqMT/7LZYQ==" });
        }
    }
}
