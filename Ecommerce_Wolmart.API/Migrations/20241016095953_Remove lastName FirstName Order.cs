using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovelastNameFirstNameOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "212005bc-6935-4033-a4d3-ad9e4da079dd", "AQAAAAIAAYagAAAAEEzeiZV3jDRkqKW6Wke/H+NgvMhVXrqLaLwaVn45qB9qAUIlkTloNGeOkRSgriHX/A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0b899767-45b5-4e1f-ad68-526962592844", "AQAAAAIAAYagAAAAENfwP6OEz0JuEncmKgdGc3MdNzTazAZQIoB8EAOgk6QUulktTlgtlGIk7VIb7tVcnA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "79379dbf-a641-4e18-83ff-8e4f7f10854f", "AQAAAAIAAYagAAAAENgzhe5Jk2M7ypMcEzJo1mRgsg2UyM2pK/vp5uVFSEkxuaRJkzbgLtrHShSKDQqPcg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ab34e3f5-d15a-41fa-9e62-88733359bb8d", "AQAAAAIAAYagAAAAEPuCpQYyVJ4/zHiTYm2ZO6Sfb4Uu6sFkbKzDpfvYjMv14IWkE76PUtsUTryGNDHyOw==" });
        }
    }
}
