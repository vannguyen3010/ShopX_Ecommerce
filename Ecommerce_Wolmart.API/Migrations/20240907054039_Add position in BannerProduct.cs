using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddpositioninBannerProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "BannerProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3048077c-13dd-48b3-8daa-a5a065b9c94b", "AQAAAAIAAYagAAAAECJ5pUJgjk50GLhTTq6q9RNWuvnA0xqm5hHD4aJ2aj9/pTy0GDS91kwfUnMf35KEkA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bcbe1352-19de-47b6-ba26-92adfd20a4a1", "AQAAAAIAAYagAAAAEB+wsErmMWdKkuGMf0p1YunhX6s+cN/XzQ/RS/30RI5g29S+ve3B1/xBl8lflzkA+A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "BannerProducts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d59c2c6b-c631-4aa1-ae13-323fcbc5a651", "AQAAAAIAAYagAAAAECW0KkfiYetGH0kgiSpX69OM2pnYrQWXIWUbaxVyT1yWxdSU8sJuepENca523Cly7w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ac78f40a-ca21-4771-aa97-c67c9043f5ed", "AQAAAAIAAYagAAAAEFflzOwdk8HTKhT8ijQ87XAxBbyjD6KveQdrTAE/5sXzipU5BkPiM8kRG544VqHiZQ==" });
        }
    }
}
