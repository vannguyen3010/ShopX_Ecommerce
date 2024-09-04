using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageFileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    ImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_CateProducts_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CateProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d99bd4bb-df96-4b9e-94a8-2a70256518d2", "AQAAAAIAAYagAAAAENKQblUIFqUrGZnQiBZzKPtquxFtzc8j0pS2ZCTQKwrbCB12rOgQU+daiwEGqhKXjw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "67d0fead-c3ee-48ff-a795-f2083aacf7e2", "AQAAAAIAAYagAAAAEG8zhixi203IRkhC8Sey6gu09fZQImRs7IJNS7KcCvpyRrgyzD2VanTRTnBhMgZaaA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4dd87908-3db3-4e3a-a9fe-0aed91821b12", "AQAAAAIAAYagAAAAEG5BSRZxGnVEPrENGC4skYnUd5O/jbDgAINYV6Sx3p6u3dWfuGlWsaUf0cCqz72qCw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6dd2150b-bf0b-470d-809e-49c310d4cdba", "AQAAAAIAAYagAAAAELLHPw9L5Oouz8311JVmx2sofd9NYXopoe9Rvwy34x2WSkXsfFl9GR4jaZkT0z/NpQ==" });
        }
    }
}
