using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTableCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "58fc05b6-8355-48a7-a2c8-2df63d5e9c53", "AQAAAAIAAYagAAAAEGOrLiNjvHEcYoI/wZlHhyocagQuRzGjgy2pyc2mfPARtfBPNlkT5CPzax7RilYXCQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c4785427-a3bf-4280-a4c4-14276e5f55cf", "AQAAAAIAAYagAAAAENjbhugwOAQGYTFefoJJWGF7Pc0AlvLk7N1HldsFbuT5kGPHBXuW/JyI4TVibWEV/g==" });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "71404ed0-ccac-4fae-9536-0265afec9bb5", "AQAAAAIAAYagAAAAEKRz3Mc8/Pde0Src+RlifuyJJGt1V5cFHUij7igI1qhYIDRWHaZ5QPpF6O3nhzBoag==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bcf7a291-d0ff-499b-b8d9-3eb0e1deed6b", "AQAAAAIAAYagAAAAEI+iSqiLZz4Mke5jpbjMxP8z56N9Xy6Nv5M7P5u/bOq0YkQZaLUKSxV1FjFoc76N7g==" });
        }
    }
}
