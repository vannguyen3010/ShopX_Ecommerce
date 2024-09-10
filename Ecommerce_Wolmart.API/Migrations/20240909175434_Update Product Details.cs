using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3a489a4c-7987-49d3-9309-52a0939a569f", "AQAAAAIAAYagAAAAEBK6B6sxQUou/bofdSatnLIbTRiU5V7bEjiiSDEIxefDHuLpbKLnCIpXaPZnRi8DwA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a22885d9-3e54-44c6-a0e0-59c0c881b5d7", "AQAAAAIAAYagAAAAEIk7LjbHq7LpHPJnLhvZs/HFvYLdp/Zvq3L8CogrIifv2XWgfYzQIpPJfo2jzYrSOA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e44d24cb-4201-46ca-b1b2-a558042675b0", "AQAAAAIAAYagAAAAEBUW5ETRF0TiulS5Mvwaf15PEEaeyHtqEBdnpRqUmWB9gBuBGTkxxzhiwL6tNKzTlA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "deae12d8-c11f-4931-8ddb-d58725c2848d", "AQAAAAIAAYagAAAAEFRIuNg95RWNVIOhOoHpkXfDkFaEgy5g2w2Vp9LlRX6nIWZzRi2zEQTR8NS6sSiFHw==" });
        }
    }
}
