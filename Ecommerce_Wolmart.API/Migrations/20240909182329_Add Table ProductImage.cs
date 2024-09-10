using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTableProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFileSizeInBytes = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f473bfee-a16b-4899-a9e4-4b9aa35db69f", "AQAAAAIAAYagAAAAEAtg0ttpjcSZnn6R+3tooEZQaQ6qT5u8u6pnhdTvOfPV7Bb6BfXsPCEGPE/YYyotSw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "388c8a05-3b69-4ebe-ac53-f840e8262ed2", "AQAAAAIAAYagAAAAEGPEKN+eEKNbx7Uv7KTySBCFllxZpYAXm1urXrcnKfhhKpDS0GJn6rZdJxvYqZzRLg==" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

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
    }
}
