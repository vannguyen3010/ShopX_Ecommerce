using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddressTypeAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressType",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressType",
                table: "Addresses");

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
    }
}
